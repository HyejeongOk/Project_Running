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

    private bool isDead = false;
    public bool isStop = false;
    public bool isHPZero = false; // ü���� 0�ΰ�?

    public bool isGiant = false; //�Ŵ�ȭ�ΰ�?
    public bool isBlast = false; //���������ΰ�?
    public bool iscrash = false;   // ��ֹ� �浹 ����

    // �Ŵ�ȭ, ��������
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
            // ���� ���� �ƴ϶�� ��ġ ����
            if(!isJump)
            {
                //�ٴں��� ���� �������� ����
                if(transform.position.y < blastY)
                {
                    Vector3 pos = transform.position;
                    pos.y = blastY;
                    transform.position = pos;

                    // �Ʒ��� �������� �ʰ� y �ӵ��� 0
                    Vector2 vel = player_r.velocity;
                    vel.y = 0f;
                    player_r.velocity = vel;

                }
            }

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
        }

        // ü���� 0�̰� �����ߴٸ� ��� ó��
        if (isHPZero && !isDead)
        {
            Die();
        }
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

    // ��ֹ� �浹
    public IEnumerator Crash_co()
    {
        animator.SetTrigger("Crash");

        // ���� ���� ó��
        //circle.enabled = false; // �ݶ��̴� ��ü�� ��

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float duration = 5f;
        float interval = 0.2f;
        float elapsed = 0f; // ����ð�

        while (elapsed < duration)
        {
            if (sprite != null)
            {
                // ������
                sprite.color = new Color(1f, 1f, 1f, 0.3f);
            }

            yield return new WaitForSeconds(interval);

            if(sprite != null)
            { 
                //�������
                sprite.color = new Color(1f, 1f, 1f, 1f);
            }
                yield return new WaitForSeconds(interval);

                elapsed += interval * 2;
        }
        // �������� ���󺹱�
        circle.enabled = true;
        iscrash = false;
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

    // �������� ������ Ȯ�� �� �ӷ� Ŀ��
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

        // ���� �ӵ��� ����
        GameManager.instance.bgSpeed = GameManager.instance.basicbgSpeed;
        GameManager.instance.mapSpeed = GameManager.instance.basicmapSpeed;
        Debug.Log($"�������� ���� �� : {GameManager.instance.bgSpeed}");

        isBlast = false;
        Blast_co = null;
    }
    #endregion
}