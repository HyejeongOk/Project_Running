using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/MapData")]
public class MapData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string MapName;  // �� �̸�
    public Sprite Mapobj;   // �� ������ �̹���
    public Sprite Mapbg_inGame; // ���ӿ� ������ �� ���
    public AudioClip Bgm;   //���
    public List<Map_Spawner> mapspawners;   //�� ����
}
