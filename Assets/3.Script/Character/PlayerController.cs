using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpforce = 100f;    // 점프 크기

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

    // 거대화, 광속질주
    public Coroutine Giant_co;
    public Coroutine Blast_co;

    private Rigidbody2D player_r;
    private Animator animator;
    private AudioSource audioSource;
    private CircleCollider2D circle;



    private HPBar hp;
    private float blastY = -4.356f;

    private void Start()
    {
        player_r = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();
        circle = GetComponent<CircleCollider2D>();

        hp = FindObjectOfType<HPBar>();

        isGrounded = true;
        animator.Play("Run_ingame");
        animator.SetBool("Grounded", true);
    }

    private void Update()
    {
        if(isStop || isDead)
        {
            return;
        }

        if(isBlast)
        {
            // 점프 중이 아니라면 위치 고정
            if(!isJump)
            {
                //바닥보다 낮게 내려가면 보정
                if(transform.position.y < blastY)
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

        }

        // 슬라이드 상태
        animator.SetBool("Sliding", isSlide);

        // 공중에 있지만 점프를 누르지 않았을 때
        animator.SetBool("Jumping", !isGrounded && isJump);

        // Grounded 상태
        animator.SetBool("Grounded", isGrounded);
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
            jumpcount++;
            isJump = true;


            isSlide = false;
            isGrounded = false;

            player_r.velocity = new Vector2(player_r.velocity.x, 0f);
            player_r.AddForce(new Vector2(0, jumpforce));

            //if(boxCollider2D !=  null)
            //{
            //    boxCollider2D.enabled = false;
            //}

            if (jumpcount ==1)
            {
                animator.SetBool("Jumping", true);
                animator.SetBool("Grounded", false);
            }

            // 2단 점프
            if(jumpcount == 2)
            {
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

        isSlide = true;
        isJump = false;
        animator.SetBool("Sliding", true);
    }

    public void OnSlideButtonExit()
    {
        isSlide = false;
        animator.SetBool("Landing", true);
    }

    // 땅에 닿았는가?
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿았음을 감지하기 위해 사용
        // 어떤 콜라이더에 닿았으며, 충돌 표면이 위쪽을 보고 있다면
        if (collision.contacts[0].normal.y > 0.1f)
        {
            if(!isGrounded)
            {
                animator.SetBool("Landing", true);
                isJump = false; // 점프 키 눌렀던 것 초기화
            }

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

    // 구멍에 빠졌을 때와 장애물에 충돌할 때 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 구멍에 빠지면 게임 종료
        if (collision.CompareTag("Hole") && !isDead)
        {
            hp.currentHP = 0;
            HPZero();
            Die();
        }

        else if(collision.CompareTag("Obstacle") && !iscrash &&!isGiant && !isBlast)
        {
            iscrash = true;

            Debug.Log(collision.gameObject.name);
            collision.enabled = false;
            StartCoroutine(Crash_co());
        }
    }

    // 장애물 충돌
    public IEnumerator Crash_co()
    {
        animator.SetTrigger("Crash");

        // 무적 상태 처리
        //circle.enabled = false; // 콜라이더 자체를 끔

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float duration = 5f;
        float interval = 0.2f;
        float elapsed = 0f; // 경과시간

        while (elapsed < duration)
        {
            if (sprite != null)
            {
                // 반투명
                sprite.color = new Color(1f, 1f, 1f, 0.3f);
            }

            yield return new WaitForSeconds(interval);

            if(sprite != null)
            { 
                //원래대로
                sprite.color = new Color(1f, 1f, 1f, 1f);
            }
                yield return new WaitForSeconds(interval);

                elapsed += interval * 2;
        }
        // 마지막엔 원상복구
        circle.enabled = true;
        iscrash = false;
    }

    // 플레이어 사망
    public void Die()
    {
        // 플레이어 사망 애니메이션 출력
        animator.SetTrigger("Die");

        // 물리 멈추기
        player_r.velocity = Vector2.zero;
        player_r.bodyType = RigidbodyType2D.Kinematic;

        isStop = true; 
        isDead = true;

        // 게임 오버
        StartCoroutine(GameManager.instance.Gameover_co());
        //GameManager.instance.isGameover = true;
    }

    // 체력이 0이라는 정보 저장
    public void HPZero()
    {
        isHPZero = true;    // 체력 0인 상태를 저장
    }

    // 일시 정지
    public void SetStop(bool stop)
    {
        isStop = stop;

        if(stop)
        {
            player_r.velocity = Vector2.zero;   // 이동 정지
            animator.enabled = false;   // 애니메이션 정지
            player_r.bodyType = RigidbodyType2D.Kinematic;  //물리 완전 정지
        }

        else
        {
            player_r.bodyType = RigidbodyType2D.Dynamic;    // 물리 다시 활성화
            animator.enabled = true;    // 애니메이션 다시 실행
        }
    }



    #region 아이템 효과
    // 거대화 아이템 획득 시 2배로 커짐
    public void ActiveGiant(float duration)
    {
        if(Giant_co != null)
        {
            StopCoroutine(Giant_co);
        }
        Giant_co = StartCoroutine(GiantMode(duration));
    }

    private IEnumerator GiantMode(float duration)
    {
        isGiant = true;
        transform.localScale = new Vector3(2f, 2f, 2f);

        yield return new WaitForSeconds(duration);

        transform.localScale = new Vector3(1f, 1f, 1f);
        isGiant = false;
        Giant_co = null;
    }

    // 광속질주 아이템 확득 시 속력 커짐
    public void ActiveBlast(float duration, float boostbg, float boostmap)
    {
        if(Blast_co != null)
        {
            StopCoroutine(Blast_co);
        }
        Blast_co = StartCoroutine(BlastMode(duration, boostbg, boostmap));
    }

    private IEnumerator BlastMode(float duration, float boostbg, float boostmap)
    {
        isBlast = true;

        GameManager.instance.bgSpeed = boostbg;
        GameManager.instance.mapSpeed = boostmap;


        float elapsed = 0f;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("2");

        // 원래 속도로 복구
        GameManager.instance.bgSpeed = GameManager.instance.basicbgSpeed;
        GameManager.instance.mapSpeed = GameManager.instance.basicmapSpeed;
        Debug.Log($"광속질주 시작 전 : {GameManager.instance.bgSpeed}");

        isBlast = false;
        Blast_co = null;
    }
    #endregion
}