using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [Header("기본 정보")]
    public GameObject jelly;    // 젤리
    public float spawner = 20f;   // 생성 위치
    public float interval = 5f;  // 생성 주기

    private void Start()
    {
        StartCoroutine(SpawnerJelly());
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
}
