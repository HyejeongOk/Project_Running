using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Spawner : MonoBehaviour
{
    public GameObject[] Map_Prefabs;
    public int count = 3;


    public float spawnXpos = 20f;   //스폰할 X 위치
    public float chunkwidth = 20f; //청크 가로 길이

    private GameObject[] Maps;
    private int currentindex = 0;

    private void Start()
    {
        Maps = new GameObject[count];

        for(int i = 0; i < count; i++)
        {
            int rndindex = Random.Range(0, Map_Prefabs.Length);
            Maps[i] = Instantiate(Map_Prefabs[rndindex], new Vector2(spawnXpos + i  * chunkwidth, 0f), Quaternion.identity);
        }
    }

    private void Update()
    {
            // 맨 앞 청크가 화면 왼쪽으로 벗어나면 재배치
            if(Maps[currentindex].transform.position.x <= -chunkwidth)
            {
            // 다음 위치로 재배치
            Maps[currentindex].transform.position = new Vector2(GetFurthestChunckX() + chunkwidth, 0f);

            int rndindex = Random.Range(0, Map_Prefabs.Length);
            }

            currentindex++;

            if(currentindex >= count)
            {
                currentindex = 0;
            }
        }

    private float GetFurthestChunckX()
    {
        float MaxX = Maps[0].transform.position.x;

        for(int i = 0; i < Maps.Length; i++)
        {
            if(Maps[i].transform.position.x > MaxX)
            {
                MaxX = Maps[i].transform.position.x;
            }
        }

        return MaxX;
    }

}
