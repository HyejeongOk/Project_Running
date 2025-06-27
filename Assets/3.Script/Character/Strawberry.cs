using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [Header("�⺻ ����")]
    public GameObject jelly;    // ����
    public float spawner = 20f;   // ���� ��ġ
    public float interval = 5f;  // ���� �ֱ�

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
