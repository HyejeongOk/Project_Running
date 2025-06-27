using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specific_Jelly : MonoBehaviour
{
    public int score = 1;
    public AudioClip jellyClip;

    private bool isCollected = false;

    [Header("끌리게 만들기")]
    private Transform player;
    public float AttractDist = 5f;
    public float AttractSpeed = 10f;

    private void Start()
    {
        //플레이어가 붙은 태그 찾기
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

    //젤리 끌리게 만들기
    private void Attractjelly()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist < AttractDist)
        {
            // 플레이어 방향 벡터
            Vector2 dirToPlayer = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

            // 무조건 플레이어 방향으로 이동
            transform.position += (Vector3)(dirToPlayer * AttractSpeed * Time.deltaTime); 
        }
    }
}
