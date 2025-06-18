using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Spawner : MonoBehaviour
{
    public GameObject[] Map_Prefabs;
    public int count = 3;

    private float TimebetSpawnMin = 0.1f;
    private float TimebetSpawnMax = 1f;
   // private float TimebetSpawn;

    public float spawnXpos = 20f;   //������ X ��ġ

    private GameObject[] Maps;
    private int currentindex = 0;

    private Vector2 poolposition = new Vector2(20f, 0f);    //��� ��ġ
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
            // ���� �������� ���
            float waitTime = Random.Range(TimebetSpawnMin, TimebetSpawnMax);
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

                //�� ���� ����
                int rndindex = Random.Range(0, Map_Prefabs.Length);
                Maps[currentindex] = Instantiate(Map_Prefabs[rndindex], new Vector2(spawnXpos, 0f), Quaternion.identity);

                //Maps[currentindex].transform.position = new Vector2(spawnXpos, 0f);

                // Scroll_map ��ũ��Ʈ �߰�
                if (Maps[currentindex].GetComponent<Scroll_Map>() == null)
                {
                    Maps[currentindex].AddComponent<Scroll_Map>();
                }

                currentindex++;

                if (currentindex >= count)
                {
                    currentindex = 0;
                }


           

        }
    }

    //private void Update()
    //{
    //    if(isPause)
    //    {
    //        return;
    //    }

    //    if (Time.time >= LastSpawnTime + TimebetSpawn)
    //    {
    //        LastSpawnTime = Time.time;  // �������� ������ �ð�
    //        TimebetSpawn = Random.Range(TimebetSpawnMin, TimebetSpawnMax);
            
    //        //�� ���� ����
    //        int rndindex = Random.Range(0, Map_Prefabs.Length);
    //        Maps[currentindex] = Instantiate(Map_Prefabs[rndindex], new Vector2(spawnXpos, 0f), Quaternion.identity);

    //        Maps[currentindex].transform.position = new Vector2(spawnXpos, 0f);

    //        // Scroll_map ��ũ��Ʈ �߰�
    //        if(Maps[currentindex].GetComponent<Scroll_Map>() == null)
    //        {
    //            Maps[currentindex].AddComponent<Scroll_Map>();
    //        }

    //        currentindex++;

    //        if (currentindex >= count)
    //        {
    //            currentindex = 0;
    //        }

    //    }
    //}

    public void SetPause(bool pause)
    {
        isPause = pause;
    }
}
