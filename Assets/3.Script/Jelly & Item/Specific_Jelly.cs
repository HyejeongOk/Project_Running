using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specific_Jelly : MonoBehaviour
{
    public int score = 1;
    public AudioClip jellyClip;

    private bool isCollected = false;

    [Header("������ �����")]
    private Transform player;
    public float AttractDist = 5f;
    public float AttractSpeed = 10f;

    private void Start()
    {
        //�÷��̾ ���� �±� ã��
        GameObject player_obj = GameObject.FindGameObjectWithTag("Player");

        player = player_obj.transform;
    }

    private void Update()
    {  
        if(player == null)
        {
            return;
        }
        Attractjelly();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isCollected)
        {
            return;
        }

        if(collision.CompareTag("Player"))
        {
            isCollected = true;
            GameManager.instance.AddScore(score);
            SFX.SoundPlay(jellyClip);
            Destroy(gameObject);
        }
    }

    //���� ������ �����
    private void Attractjelly()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist < AttractDist)
        {
            // �÷��̾� ���� ����
            Vector2 dirToPlayer = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

            // ������ �÷��̾� �������� �̵�
            transform.position += (Vector3)(dirToPlayer * AttractSpeed * Time.deltaTime); 
        }
    }
}
