using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Spawner : MonoBehaviour
{
    public GameObject[] Map_Prefabs;
    public int count = 5;

    // 시간 간격
    private float TimebetSpawnMin = 0.5f;
    private float TimebetSpawnMax = 1.5f;

    public float spawnXpos = 50f;   //스폰할 X 위치

    private GameObject[] Maps;

    private Vector2 poolposition = new Vector2(10f, 0f);    //대기 위치
    private GameObject LastSpawnedMap;  // 마지막으로 생성한 맵 추적

    private Coroutine spawn_co;
    private bool isPause = false;

    private void Start()
    {
        Maps = new GameObject[count];

        int rndindex = Random.Range(0, Map_Prefabs.Length);
        Maps[0] = Instantiate(Map_Prefabs[rndindex], poolposition, Quaternion.identity);

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

            // 광속질주 등 속도 변화에 따라 대기시간 보정
            // 현재 배경 무한스크롤 방식이기 때문에 맵이 빨리 움직이면 이에 따라서 맵 스폰도 시간도 빨라야한다.
            // 속도 = 거리 / 시간 // 시간 = 거리 / 속력
            float baseSpeed = 5f;   // 초기 기본속도
            float currentSpeed = GameManager.instance.mapSpeed;
            float speedRatio = currentSpeed / baseSpeed;    // 얼마나 빠른가?

            //만약 초기 속도랑 현재 속도가 같으면 speedRatio가 1이 된다
            // 광속질주로 현재 속도가 10이 되면 speedRatio가 2가 된다. => 속도가 빨라졌다
            // 나눗셈 => 속도가 빠를수록 대기시간도 짧아진다
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
