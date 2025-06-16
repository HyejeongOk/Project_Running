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

    private Rigidbody player_r;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        player_r = transform.GetComponent<Rigidbody>();
        animator = transform.GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpcount < 2)
        {
            jumpcount++;
            player_r.velocity = Vector2.zero;
            player_r.AddForce(new Vector2(0, jumpforce));

            //audioSource.Play();
        }

        else if(Input.GetKeyUp(KeyCode.Space) && player_r.velocity.y > 0)
        {
            player_r.velocity = player_r.velocity * 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        iscrash = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        iscrash = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ������� �����ϱ� ���� ���
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� �ִٸ�
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpcount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
