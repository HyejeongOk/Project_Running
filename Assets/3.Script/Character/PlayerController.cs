using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpforce = 100f;    // ���� ũ��

    private int jumpcount = 0;

    private bool isGrounded = true;    // ���� ��Ҵ°�?
    private bool isJump = false;    // ���� ���ΰ�?
    private bool isSlide = false;   //�����̵� ����
    private bool iscrash = false;   // ��ֹ� �浹 ����

    //private bool isJumpRequest = false;


    private bool isDead = false;
    public bool isStop = false;
    public bool isHPZero = false; // ü���� 0�ΰ�?

    private Rigidbody2D player_r;
    private Animator animator;
    private AudioSource audioSource;

    //private BoxCollider2D boxCollider2D;
    //private CircleCollider2D circleCollider2D;

    private void Start()
    {
        player_r = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();

        // �ݶ��̴� ĳ��
        //boxCollider2D = GetComponent<BoxCollider2D>();
        //circleCollider2D = GetComponent<CircleCollider2D>();

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

        // �����̵� ����
        animator.SetBool("Sliding", isSlide);

        // ���߿� ������ ������ ������ �ʾ��� ��
        animator.SetBool("Jumping", !isGrounded && isJump);

        // Grounded ����
        animator.SetBool("Grounded", isGrounded);
    }

    // ����Ű�� ������ ���� �۵�
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

            // 2�� ����
            if(jumpcount == 2)
            {
                animator.SetTrigger("DoubleJump");
            }
        }
    }

    // �����̵�Ű�� ������ �����̵�
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

    // ���� ��Ҵ°�?
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ������� �����ϱ� ���� ���
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� �ִٸ�
        if (collision.contacts[0].normal.y > 0.1f)
        {
            if(!isGrounded)
            {
                animator.SetBool("Landing", true);
                isJump = false; // ���� Ű ������ �� �ʱ�ȭ
            }

            isGrounded = true;
            jumpcount = 0;

            // �ٽ� �浹 Ȱ��ȭ
            //if (boxCollider2D != null)
            //{
            //    boxCollider2D.enabled = true;
            //}
        }

        // ü���� 0�̰� �����ߴٸ� ��� ó��
        if (isHPZero && !isDead)
        {
            Die();
        }
    }

    public void OnGrounded()
    {
        isGrounded = true;
        animator.SetBool("Grounded", true);
        jumpcount = 0;
    }

    public void OnLeaveGround()
    {
        isGrounded = false;
        animator.SetBool("Grounded", false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    // ���ۿ� ������ ���� ��ֹ��� �浹�� �� 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���ۿ� ������ ���� ����
        if (collision.CompareTag("Hole") && !isDead)
        {
            Die();
        }
    }

    // �÷��̾� ���
    public void Die()
    {
        // �÷��̾� ��� �ִϸ��̼� ���
        animator.SetTrigger("Die");

        // ���� ���߱�
        player_r.velocity = Vector2.zero;
        player_r.bodyType = RigidbodyType2D.Kinematic;

        isStop = true; 
        isDead = true;

        // ���� ����
        StartCoroutine(GameManager.instance.Gameover_co());
        //GameManager.instance.isGameover = true;
    }

    // ü���� 0�̶�� ���� ����
    public void HPZero()
    {
        isHPZero = true;    // ü�� 0�� ���¸� ����
    }

    // �Ͻ� ����
    public void SetStop(bool stop)
    {
        isStop = stop;

        if(stop)
        {
            player_r.velocity = Vector2.zero;   // �̵� ����
            animator.enabled = false;   // �ִϸ��̼� ����
            player_r.bodyType = RigidbodyType2D.Kinematic;  //���� ���� ����
        }

        else
        {
            player_r.bodyType = RigidbodyType2D.Dynamic;    // ���� �ٽ� Ȱ��ȭ
            animator.enabled = true;    // �ִϸ��̼� �ٽ� ����
        }
    }



    #region ������ ȿ��
    // �Ŵ�ȭ ������ ȹ�� �� 2��� Ŀ��
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

    // �������� ������ Ȯ�� �� �ӷ� Ŀ��
    public void ActiveBlast(float duration, float boostbg, float boostmap)
    {
        StartCoroutine(BlastMode(duration, boostbg, boostmap));
    }

    private IEnumerator BlastMode(float duration, float boostbg, float boostmap)
    {
        float bgSpeed = GameManager.instance.bgSpeed;
        float mapSpeed = GameManager.instance.mapSpeed;

        Debug.Log($"�������� ���� �� : {bgSpeed}");
        Debug.Log($"�������� ���� �� : {GameManager.instance.bgSpeed}");
        GameManager.instance.bgSpeed = boostbg;
        GameManager.instance.mapSpeed = boostmap;
        Debug.Log("1");

        float elapsed = 0f;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Debug.Log(GameManager.instance.bgSpeed);
             Debug.Log($"��������! : {elapsed} / {duration}");
            yield return null;
        }
        Debug.Log("2");

        GameManager.instance.bgSpeed = bgSpeed;
        GameManager.instance.mapSpeed = mapSpeed;
        Debug.Log($"�������� ���� �� : {GameManager.instance.bgSpeed}");
    }
    #endregion
}