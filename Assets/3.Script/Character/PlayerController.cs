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
        //바닥에 닿았음을 감지하기 위해 사용
        // 어떤 콜라이더에 닿았으며, 충돌 표면이 위쪽을 보고 있다면
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
