using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/MapData")]
public class MapData : ScriptableObject
{
    [Header("기본 정보")]
    public string MapName;  // 맵 이름
    public Sprite Mapobj;   // 맵 프리뷰 이미지
    public Sprite Mapbg_inGame; // 게임에 적용할 맵 배경
    public AudioClip Bgm;   //브금
    public List<Map_Spawner> mapspawners;   //맵 스폰
}
