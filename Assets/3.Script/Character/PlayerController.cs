using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpforce = 100f;    // ���� ũ��

    private int jumpcount = 0;

    private bool isGrounded = false;
    private bool iscrash = false;
    private bool isDead = false;
    public bool isStop = false;
    public bool isHPZero = false; // ü���� 0�ΰ�?

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

    // ���ۿ� ������ ���� ��ֹ��� �浹�� �� 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���ۿ� ������ ���� ����
        if (collision.CompareTag("Hole") && !isDead)
        {
            Die();
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ������� �����ϱ� ���� ���
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� �ִٸ�
        if (collision.contacts[0].normal.y > 0.1f)
        {
            isGrounded = true;
            jumpcount = 0;
        }

        // ü���� 0�̰� �����ߴٸ� ��� ó��
        if(isHPZero && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

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
}