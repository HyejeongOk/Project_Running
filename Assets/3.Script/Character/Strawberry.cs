using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [Header("기본 정보")]
    public GameObject jelly;    // 젤리
    public float spawner = 20f;   // 생성 위치
    public float interval = 5f;  // 생성 주기

    [Header("끌리게 만들기")]
    public Transform player;
    public float AttractDist = 0.2f;   //끌릴 수 있는 거리
    public float AttractSpeed = 10f;  //끌리는 속도

    private void Start()
    {
        StartCoroutine(SpawnerJelly());
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }

        Attractjelly();
    }

    private IEnumerator SpawnerJelly()
    {
        while(true)
        {
            if(GameManager.instance.isGameover)
            {
                yield break;
            }

            yield return new WaitForSeconds(interval);

            Vector2 spawn = (Vector2)transform.position + (Vector2)transform.right * spawner;

            Instantiate(jelly, spawn, Quaternion.identity);
        }
    }

    private void Attractjelly()
    {
        GameObject[] jellies = GameObject.FindGameObjectsWithTag("Jelly");

        foreach (var j in jellies)
        {
            float dist = Vector2.Distance(player.position, j.transform.position);

            if(dist < AttractDist)
            {
                // 플레리어 방향 벡터
                Vector2 dirToPlayer = ((Vector2)player.position - (Vector2)j.transform.position).normalized;

                // 무조건 플레이어 방향으로 이동
                j.transform.position += (Vector3)(dirToPlayer * AttractSpeed * Time.deltaTime);
            }
        }

    }
}
