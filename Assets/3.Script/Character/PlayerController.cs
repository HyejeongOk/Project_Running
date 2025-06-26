using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpforce = 100f;    // 점프 크기

    //오디오
    [SerializeField] private AudioClip Jumpclip;
    [SerializeField] private AudioClip Slideclip;
    [SerializeField] private AudioClip Crashclip;

    private int jumpcount = 0;

    private bool isGrounded = true;    // 땅에 닿았는가?
    private bool isJump = false;    // 점프 중인가?
    private bool isSlide = false;   //슬라이드 여부

    private bool isDead = false;
    public bool isStop = false;
    public bool isHPZero = false; // 체력이 0인가?

    public bool isGiant = false; //거대화인가?
    public bool isBlast = false; //광속질주인가?
    public bool iscrash = false;   // 장애물 충돌 여부

    // 무적 상태
    public bool isAbsoluteInvincible = false;  //광속질주 / 거대화 무적
    public bool isBlinkInvincible = false; // 깜빡임 무적
    public bool isInvincible => isAbsoluteInvincible || isBlinkInvincible;

    // 거대화, 광속질주 코루틴
    public Coroutine Giant_co;  // 거대화 코루틴
    public Coroutine Blast_co;  // 광속질주 코루틴
    public Coroutine Blink_co;  // 깜빡임 코루틴

    private Rigidbody2D player_r;
    private Animator animator;
    private AudioSource audioSource;

    private HPBar hp;
    private float blastY = -4.356f;

    private void Start()
    {
        player_r = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();

        hp = FindObjectOfType<HPBar>();

        isGrounded = true;
        animator.Play("Run_ingame");
        animator.SetBool("Grounded", true);
    }

    private void Update()
    {
        if (PauseManager.instance.IsPaused() || isDead)
        {
            return;
        }

        if (isBlast || isInvincible)
        {
            // 점프 중이 아니라면 위치 고정
            if(!isJump)
            {
                CrossHole();
            }
        }

        if (isJump && isBlast)
        {
            // 구름 다리를 걷는 함수
            CrossHole();

            // 점프 해제
            isJump = false;
            isGrounded = true;
            jumpcount = 0;
            return;
        }

        // 슬라이드 상태
        animator.SetBool("Sliding", isSlide);

        // 공중에 있지만 점프를 누르지 않았을 때
        animator.SetBool("Jumping", !isGrounded && isJump);

        // Grounded 상태
        animator.SetBool("Grounded", isGrounded);
    }

    // 구멍에 떨어지지 않게 -> 구름다리 걷는 것처럼
    public void CrossHole()
    {
        //바닥보다 낮게 내려가면 보정
        if (transform.position.y < blastY)
        {
            Vector3 pos = transform.position;
            pos.y = blastY;
            transform.position = pos;

            // 아래로 떨어지지 않게 y 속도만 0
            Vector2 vel = player_r.velocity;
            vel.y = 0f;
            player_r.velocity = vel;

        }
    }

    // 점프키를 누르면 점프 작동
    public void OnJumpButton()
    {

        if (isStop || isDead)
        {
            return;
        }

        if(jumpcount < 2)
        {
            audioSource.clip = Jumpclip;
            audioSource.Play();
            jumpcount++;
            isJump = true;

            isSlide = false;
            isGrounded = false;

            player_r.velocity = new Vector2(player_r.velocity.x, 0f);
            player_r.AddForce(new Vector2(0, jumpforce));

            if (jumpcount ==1)
            {
                animator.SetBool("Jumping", true);
                animator.SetBool("Grounded", false);
            }

            // 2단 점프
            if(jumpcount == 2)
            {
                animator.SetBool("Jumping", true);
                animator.SetBool("Grounded", false);
                animator.SetTrigger("DoubleJump");
            }
        }
    }

    // 슬라이드키를 누르면 슬라이드
    public void OnSlideButton()
    {
        if (isStop || isDead)
        {
            return;
        }
        audioSource.clip = Slideclip;
        audioSource.Play();
        isSlide = true;
        isJump = false;
        animator.SetBool("Sliding", true);
    }

    public void OnSlideButtonExit()
    {
        isSlide = false;
    }

    // 땅에 닿았는가?
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿았음을 감지하기 위해 사용
        // 어떤 콜라이더에 닿았으며, 충돌 표면이 위쪽을 보고 있다면
        if (collision.contacts[0].normal.y > 0.1f)
        {
            isJump = false; 
            isGrounded = true;

            jumpcount = 0;
        }

        // 체력이 0이고 착지했다면 사망 처리
        if (isHPZero && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    // 구멍에 빠졌을 때와 장애물에 충돌할 때 , 젤리 획득
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 구멍에 빠지면 게임 종료
        if (collision.CompareTag("Hole") && !isDead)
        {
            hp.currentHP = 0;
            HPZero();
            Die();
            return;
        }

        if (isInvincible)
        {
            return;
        }

        else if (collision.CompareTag("Obstacle") && !iscrash && !isGiant && !isBlast && !isInvincible)
        {
            iscrash = true;

            audioSource.clip = Crashclip;
            audioSource.Play();

            animator.SetTrigger("Crash");
            StartBlinkInvincible(5f);
        }
    }

    private void StartBlinkInvincible(float duration)
    {
        if(GameManager.instance.isGameover)
        {
            return;
        }

        if(Blink_co != null)
        {
            StopCoroutine(Blink_co);
            Blast_co = null;
            ResetPlayerAlpha();
            isBlinkInvincible = false;

        }
         Blink_co = StartCoroutine(blink_co(duration));
    }
    private IEnumerator blink_co(float duration)
    {
        isBlinkInvincible = true;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float interval = 0.2f;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            if(PauseManager.instance.IsPaused() || isStop)
            {
                yield return null;
                continue;
            }

            // 깜빡임 
            if (sprite != null)
            {
                // 반투명
                sprite.color = new Color(1f, 1f, 1f, 0.3f);
            }

            yield return new WaitForSeconds(interval);

            if (sprite != null)
            {
                //원래대로
                sprite.color = new Color(1f, 1f, 1f, 1f);
            }
            yield return new WaitForSeconds(interval);

            elapsed += interval * 2;
        }

        sprite.color = new Color(1f, 1f, 1f, 1f);
        isBlinkInvincible = false;
        iscrash = false;
        Blink_co = null;
    }

    #region 아이템 효과
    // 거대화 아이템 획득 시 2배로 커짐
    public void ActiveGiant(float duration)
    {
        if (Blink_co != null)
        {
            StopCoroutine(Blink_co);
            Blink_co = null;
            isBlinkInvincible = false;
            ResetPlayerAlpha();
            //경과시간 초기화
            //invincibleelapsed = 0f;
        }

        isAbsoluteInvincible = true;    // 거대화로 인한 무적 (광속질주 시 광속질주로 인한 무적)

        if (Giant_co != null)
        {
            StopCoroutine(Giant_co);
        }

        Giant_co = StartCoroutine(GiantMode(duration));
    }

    private IEnumerator GiantMode(float duration)
    {
        isGiant = true;
        transform.DOScale(2f, 1f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (PauseManager.instance.IsPaused())
            {
                yield return null;
                continue;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSeconds(duration);
        isGiant = false;
        isAbsoluteInvincible = false;
        if (!isBlast && !isBlinkInvincible)
        {
            StartBlinkInvincible(5f);
        }

        //isstopInvincible = false;   //일시정지 해제
        //StartInvincible_co();   // 무적 새로 시작

        Giant_co = null;
        transform.DOScale(1f, 1f);
    }

    // 광속질주 아이템 확득 시 속력 커짐
    public void ActiveBlast(float duration, float boostbg, float boostmap)
    {
     if(Blink_co != null)
        {
            StopCoroutine(Blink_co);
            Blink_co = null;
            isBlinkInvincible = false;
            ResetPlayerAlpha();
        }

        isAbsoluteInvincible = true;

        if (Blast_co != null)
        {
            // 이전 광속질주 코루틴 종료
            StopCoroutine(Blast_co);
            //Blast_co = null;
        }

        Blast_co = StartCoroutine(BlastMode(duration, boostbg, boostmap));
    }

    private IEnumerator BlastMode(float duration, float boostbg, float boostmap)
    {
        isBlast = true;

        GameManager.instance.bgSpeed = boostbg;
        GameManager.instance.mapSpeed = boostmap;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (PauseManager.instance.IsPaused())
            {
                GameManager.instance.bgSpeed = 0f;
                GameManager.instance.mapSpeed = 0f;

                yield return null;
                continue;
            }

            GameManager.instance.bgSpeed = boostbg;
            GameManager.instance.mapSpeed = boostmap;

            elapsed += Time.deltaTime;
            yield return null;
        }

        isBlast = false;

        if(isGiant)
        {
            isAbsoluteInvincible = true;
        }
        isAbsoluteInvincible = false;

        if(!isGiant && !isBlinkInvincible)
        {
            StartBlinkInvincible(5f);
        }

        Blast_co = null;
        // 원래 속도로 복구
        GameManager.instance.bgSpeed = GameManager.instance.basicbgSpeed;
        GameManager.instance.mapSpeed = GameManager.instance.basicmapSpeed;

    }
    #endregion

    // 스프라이트 캐릭터 알파값 초기화
    private void ResetPlayerAlpha()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if(sprite != null)
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);   // 완전 불투명
        }
    }

    // 플레이어 사망
    public void Die()
    {
        transform.DOKill(); // 실행 중인 tween 안전하게 제거

        // 플레이어 사망 애니메이션 출력
        animator.SetTrigger("Die");

        // 물리 멈추기
        player_r.velocity = Vector2.zero;
        player_r.bodyType = RigidbodyType2D.Kinematic;

        isStop = true; 
        isDead = true;

        // 게임 오버
        StartCoroutine(GameManager.instance.Gameover_co());
    }

    // 체력이 0이라는 정보 저장
    public void HPZero()
    {
        isHPZero = true;    // 체력 0인 상태를 저장
    } 
}