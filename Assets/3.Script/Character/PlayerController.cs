using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpforce = 100f;    // 점프 크기

    private int jumpcount = 0;

    private bool isGrounded = false;
    private bool iscrash = false;
    private bool isDead = false;
    public bool isStop = false;
    public bool isHPZero = false; // 체력이 0인가?

    private Rigidbody2D player_r;
    private Animator animator;
    private AudioSource audioSource;

    private HPBar hp;

    private void Start()
    {
        player_r = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(isStop)
        {
            return;
        }

        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpcount < 2)
        {
            jumpcount++;
            player_r.velocity = Vector2.zero;
            player_r.AddForce(new Vector2(0, jumpforce));
            //audioSource.Play();
        }

        else if (Input.GetKeyUp(KeyCode.Space) && player_r.velocity.y > 0)
        {
            player_r.velocity = player_r.velocity * 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);
    }

    // 구멍에 빠졌을 때와 장애물에 충돌할 때 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 구멍에 빠지면 게임 종료
        if (collision.CompareTag("Hole") && !isDead)
        {
            Die();
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿았음을 감지하기 위해 사용
        // 어떤 콜라이더에 닿았으며, 충돌 표면이 위쪽을 보고 있다면
        if (collision.contacts[0].normal.y > 0.1f)
        {
            isGrounded = true;
            jumpcount = 0;
        }

        // 체력이 0이고 착지했다면 사망 처리
        if(isHPZero && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    // 거대화 아이템 획득 시 2배로 커짐
    public void ActiveGiant(float duration)
    {
        StartCoroutine(GiantMode(duration));
    }

    private IEnumerator GiantMode(float duration)
    {
        transform.localScale = new Vector3(2f, 2f, 2f);

        yield return new WaitForSeconds(duration);

        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // 광속질주 아이템 확득 시 속력 커짐
    public void ActiveBlast(float duration, float boostbg, float boostmap)
    {
        StartCoroutine(BlastMode(duration, boostbg, boostmap));
    }

    private IEnumerator BlastMode(float duration, float boostbg, float boostmap)
    {
        float bgSpeed = GameManager.instance.bgSpeed;
        float mapSpeed = GameManager.instance.mapSpeed;

        Debug.Log($"광속질주 시작 전 : {bgSpeed}");
        Debug.Log($"광속질주 시작 전 : {GameManager.instance.bgSpeed}");
        GameManager.instance.bgSpeed = boostbg;
        GameManager.instance.mapSpeed = boostmap;
        Debug.Log("1");

        float elapsed = 0f;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Debug.Log(GameManager.instance.bgSpeed);
             Debug.Log($"광속질주! : {elapsed} / {duration}");
            yield return null;
        }
        Debug.Log("2");

        GameManager.instance.bgSpeed = bgSpeed;
        GameManager.instance.mapSpeed = mapSpeed;
        Debug.Log($"광속질주 시작 전 : {GameManager.instance.bgSpeed}");
    }
}