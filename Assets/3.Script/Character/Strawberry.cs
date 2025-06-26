using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [Header("�⺻ ����")]
    public GameObject jelly;    // ����
    public float spawner = 20f;   // ���� ��ġ
    public float interval = 5f;  // ���� �ֱ�

    [Header("������ �����")]
    public Transform player;
    public float AttractDist;   //���� �� �ִ� �Ÿ�
    public float AttractSpeed;  //������ �ӵ�

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

            Instantiate(jelly, spawn, Quaternion.identity);
        }
    }

    private void Attractjelly()
    {
        GameObject[] jellies = GameObject.FindGameObjectsWithTag("Jelly");

        foreach(var j in jellies)
        {
            float dist = Vector2.Distance(player.position, j.transform.position);
            if(dist < AttractDist)
            {
                // ������ ĳ���� �������� �̵�
                Vector2 dir = ((Vector2)player.transform.position - (Vector2)j.transform.position).normalized;
                j.transform.position += (Vector3)(dir * AttractSpeed * Time.deltaTime);
            }
        }
    }
}
