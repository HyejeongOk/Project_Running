using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName ="ScriptableObject/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string CharacterName;
    public GameObject stand_obj;
    public GameObject run_obj;
    public GameObject Grade;

    [Header("�ΰ���")]
    public GameObject ingame_obj;

    [Header("�κ��")]
    public GameObject Lobby_obj;
}
