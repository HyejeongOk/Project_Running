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

    public float spawnXpos = 50f;   //스폰할 X 위치

    private GameObject[] Maps;

    private Vector2 poolposition = new Vector2(20f, 0f);    //대기 위치
    private GameObject LastSpawnedMap;  // 마지막으로 생성한 맵 추적
   // private float LastSpawnTime;

    private Coroutine spawn_co;
    private bool isPause = false;

    private void Start()
    {
        Maps = new GameObject[count];

        //초기화
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
            //만약 게임이 종료되면 coroutine 멈춤
            if(GameManager.instance.isGameover)
            {
                yield break;
            }

            // 다음 스폰까지 대기

            // 광속질주 등 속도 변화에 따라 대기시간 보정
            float baseSpeed = 5f;   // 초기 기본속도
            float currentSpeed = GameManager.instance.mapSpeed;
            float speedRatio = currentSpeed / baseSpeed;

            float waitTime = Random.Range(TimebetSpawnMin, TimebetSpawnMax) / speedRatio;

            // 대기 중에도 멈춤
            float elapsed = 0f;

            while(elapsed < waitTime)
            {
                if (!isPause)
                {
                    elapsed += Time.deltaTime;
                }
                yield return null;
            }

            //만약 대기가 끝났을 때도 isPause면 스폰 패스하고 다음 프레임으로
            if(isPause)
            {
                continue;
            }

            // 이전 맵이 충분히 왼쪽으로 이동했는지 확인
            if(LastSpawnedMap != null)
            {
                while(LastSpawnedMap.transform.position.x > 0f)
                {
                    yield return null;  // 이전 맵이 더 이동할 때까지 대기
                }
            }

            //맵 랜덤 변수
            int rndindex = Random.Range(0, Map_Prefabs.Length);
            GameObject newMap = Instantiate(Map_Prefabs[rndindex], new Vector2(spawnXpos, 0f), Quaternion.identity);

            //Maps[currentindex].transform.position = new Vector2(spawnXpos, 0f);

            // Scroll_map 스크립트 추가
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
