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
    public float AttractDist;
    public float AttractSpeed;

    private void Start()
    {
        StartCoroutine(SpawnerJelly());
    }

    private void Update()
    {
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

            GameObject obj = Instantiate(jelly, spawn, Quaternion.identity);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                rb.velocity = new Vector2(-GameManager.instance.mapSpeed, 0f);
            }

        }
    }

    private void Attractjelly()
    {
        GameObject[] jellies = GameObject.FindGameObjectsWithTag("Jelly");

        foreach(var j in jellies)
        {
            float dist = Vector2.Distance(transform.position, j.transform.position);
            if(dist < AttractDist)
            {
                // 젤리를 캐릭터 방향으로 이동
                Vector2 dir = ((Vector2)transform.position - (Vector2)j.transform.position).normalized;
                j.transform.position += (Vector3)(dir * AttractSpeed * Time.deltaTime);
            }
        }
    }
}
