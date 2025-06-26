using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Spawner : MonoBehaviour
{
    public GameObject[] Map_Prefabs;
    public int count = 3;

    private float TimebetSpawnMin = 0f;
    private float TimebetSpawnMax = 2f;
   // private float TimebetSpawn;

    public float spawnXpos = 50f;   //������ X ��ġ

    private GameObject[] Maps;

    private Vector2 poolposition = new Vector2(20f, 0f);    //��� ��ġ
    private GameObject LastSpawnedMap;  // ���������� ������ �� ����
   // private float LastSpawnTime;

    private Coroutine spawn_co;
    private bool isPause = false;

    private void Start()
    {
        Maps = new GameObject[count];

        //�ʱ�ȭ
        for (int i = 0; i < count; i++)
        {
            int rndindex = Random.Range(0, Map_Prefabs.Length);
            Maps[i] = Instantiate(Map_Prefabs[rndindex], poolposition, Quaternion.identity);
        }
        //LastSpawnTime = Time.time;
        //TimebetSpawn = Random.Range(TimebetSpawnMin, TimebetSpawnMax);

        spawn_co = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            //���� ������ ����Ǹ� coroutine ����
            if(GameManager.instance.isGameover)
            {
                yield break;
            }

            // ���� �������� ���

            // �������� �� �ӵ� ��ȭ�� ���� ���ð� ����
            float baseSpeed = 5f;   // �ʱ� �⺻�ӵ�
            float currentSpeed = GameManager.instance.mapSpeed;
            float speedRatio = currentSpeed / baseSpeed;

            float waitTime = Random.Range(TimebetSpawnMin, TimebetSpawnMax) / speedRatio;

            // ��� �߿��� ����
            float elapsed = 0f;

            while(elapsed < waitTime)
            {
                if (!isPause)
                {
                    elapsed += Time.deltaTime;
                }
                yield return null;
            }

            //���� ��Ⱑ ������ ���� isPause�� ���� �н��ϰ� ���� ����������
            if(isPause)
            {
                continue;
            }

            // ���� ���� ����� �������� �̵��ߴ��� Ȯ��
            if(LastSpawnedMap != null)
            {
                while(LastSpawnedMap.transform.position.x > 0f)
                {
                    yield return null;  // ���� ���� �� �̵��� ������ ���
                }
            }

            //�� ���� ����
            int rndindex = Random.Range(0, Map_Prefabs.Length);
            GameObject newMap = Instantiate(Map_Prefabs[rndindex], new Vector2(spawnXpos, 0f), Quaternion.identity);

            //Maps[currentindex].transform.position = new Vector2(spawnXpos, 0f);

            // Scroll_map ��ũ��Ʈ �߰�
            if (newMap.GetComponent<Scroll_Map>() == null)
            {
                newMap.AddComponent<Scroll_Map>();
            }

            LastSpawnedMap = newMap;

            yield return null;

           

        }
    }

    public void SetPause(bool pause)
    {
        isPause = pause;
    }
}
