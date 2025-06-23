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

    // ���� ����
    public bool isAbsoluteInvincible = false;  //�������� / �Ŵ�ȭ ����
    public bool isBlinkInvincible = false; // ������ ����
    public bool isInvincible => isAbsoluteInvincible || isBlinkInvincible;

    // �Ŵ�ȭ, �������� �ڷ�ƾ
    public Coroutine Giant_co;  // �Ŵ�ȭ �ڷ�ƾ
    public Coroutine Blast_co;  // �������� �ڷ�ƾ
    public Coroutine Blink_co;  // ������ �ڷ�ƾ

    //���� �ڷ�ƾ
    //public Coroutine Invincibleco = null; // ���� �ڷ�ƾ

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
            return;
        }

        if (isInvincible)
        {
            return;
        }

        else if(collision.CompareTag("Obstacle") && !iscrash &&!isGiant && !isBlast && !isInvincible)
        {
            Debug.Log("�浹");
            iscrash = true;

            animator.SetTrigger("Crash");
            StartBlinkInvincible(5f);
        }
    }

    // ���� ���� �ڷ�ƾ ����
    //public void StartInvincible_co(float duration = 5f)
    //{
    //    if(GameManager.instance.isGameover)
    //    {
    //        return;
    //    }

    //    if(Blink_co!= null)
    //    {
    //        StopCoroutine(Blink_co);
    //    }
    //    Blink_co = StartCoroutine(Invincible_co(duration));
    //}

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

            // ������ 
            if (sprite != null)
            {
                // ������
                sprite.color = new Color(1f, 1f, 1f, 0.3f);
                Debug.Log("1. ������");
            }

            yield return new WaitForSeconds(interval);

            if (sprite != null)
            {
                //�������
                sprite.color = new Color(1f, 1f, 1f, 1f);
                Debug.Log("2. �������");
            }
            yield return new WaitForSeconds(interval);

            elapsed += interval * 2;
        }

        sprite.color = new Color(1f, 1f, 1f, 1f);
        isBlinkInvincible = false;
        iscrash = false;
        Blink_co = null;
    }

    #region ������ ȿ��
    // �Ŵ�ȭ ������ ȹ�� �� 2��� Ŀ��
    public void ActiveGiant(float duration)
    {
        if (Blink_co != null)
        {
            StopCoroutine(Blink_co);
            Blink_co = null;
            isBlinkInvincible = false;
            ResetPlayerAlpha();
            //����ð� �ʱ�ȭ
            //invincibleelapsed = 0f;
        }

        isAbsoluteInvincible = true;    // �Ŵ�ȭ�� ���� ���� (�������� �� �������ַ� ���� ����)

        if (Giant_co != null)
        {
            StopCoroutine(Giant_co);
        }

        Giant_co = StartCoroutine(GiantMode(duration));
    }

    private IEnumerator GiantMode(float duration)
    {
        isGiant = true;
        transform.localScale = new Vector3(2f, 2f, 2f);

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

        //isstopInvincible = false;   //�Ͻ����� ����
        //StartInvincible_co();   // ���� ���� ����

        Giant_co = null;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // �������� ������ Ȯ�� �� �ӷ� Ŀ��
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
            // ���� �������� �ڷ�ƾ ����
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
        isAbsoluteInvincible = false;
        if(!isGiant && !isBlinkInvincible)
        {
            StartBlinkInvincible(5f);
        }

        Blast_co = null;
        // ���� �ӵ��� ����
        GameManager.instance.bgSpeed = GameManager.instance.basicbgSpeed;
        GameManager.instance.mapSpeed = GameManager.instance.basicmapSpeed;
        Debug.Log($"�������� ���� �� : {GameManager.instance.bgSpeed}");


        // �Ŵ�ȭ ���� �ƴϸ� ����ó��
        //if (!isGiant)
        //{
        //    StartInvincible_co();
        //}

        //else 
        //{
        //    isstopInvincible = false;   //�Ͻ����� ����
        //    // �Ŵ�ȭ ���̸� ���� �ڷ�ƾ�� ���� ������ �����ϰų� ���� ó�� �ʱ�ȭ
        //    if (Invincibleco != null)
        //    {
        //        StopCoroutine(Invincibleco);
        //        Invincibleco = null;
        //        ResetPlayerAlpha();
        //        isInvincible = false;   //�Ͻ����� ����
        //        invincibleelapsed = 0f;
        //    }
        //}

        // ���� �ڷ�ƾ ���� ����
        //if (!isGiant && !iscrash && !isInvincible)
        //{
        //    StartInvincible_co();
        //}

    }
    #endregion

    // ��������Ʈ ĳ���� ���İ� �ʱ�ȭ
    private void ResetPlayerAlpha()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if(sprite != null)
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);   // ���� ������
        }
    }

    //private void CheckInvincibleEnd()
    //{
    //    if(!isGiant && !isBlast)
    //    {
    //        StartInvincible_co();   // ������ ���� ����
    //    }    
    //}

    // ������ ���� �ߴ�
    //private void StopBlink()
    //{
    //    if(Invincibleco != null)
    //    {
    //        StopCoroutine(Invincibleco);
    //        Invincibleco = null;
    //    }
    //    ResetPlayerAlpha();
    //}

    //// ���� ó��
    //public IEnumerator Invincible_co(float duration)
    //{
    //    Debug.Log("����");
    //    isInvincible = true;
    //    invincibletime = duration;
    //    invincibleelapsed = 0f;

    //    SpriteRenderer sprite = GetComponent<SpriteRenderer>();
    //    float interval = 0.2f;

    //    while (invincibleelapsed < invincibletime)
    //    {
    //        if(PauseManager.instance.IsPaused() || isstopInvincible || isStop)
    //        {
    //            yield return null;
    //            continue;
    //        }

    //        // ������ 
    //        if (sprite != null)
    //        {
    //            // ������
    //            sprite.color = new Color(1f, 1f, 1f, 0.3f);
    //            Debug.Log("1. ������");
    //        }

    //        yield return new WaitForSeconds(interval);

    //        if (PauseManager.instance.IsPaused() || isstopInvincible || isStop)
    //        {
    //            yield return null;
    //            continue;
    //        }

    //        if (sprite != null)
    //        {
    //            //�������
    //            sprite.color = new Color(1f, 1f, 1f, 1f);
    //            Debug.Log("2. �������");
    //        }
    //        yield return new WaitForSeconds(interval);

    //        invincibleelapsed += interval * 2;
    //    }
    //    // �������� ���󺹱� -> ��������
    //    ResetPlayerAlpha();

    //    isInvincible = false;
    //    Invincibleco = null;
    //    invincibleelapsed = 0f;
    //    isstopInvincible = false;
    //    iscrash = false;
    //}

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

   
}