using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName ="ScriptableObject/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public string CharacterName;
    public GameObject stand_obj;
    public GameObject run_obj;
    public GameObject Grade;

    [Header("인게임")]
    public GameObject ingame_obj;

    [Header("로비씬")]
    public GameObject Lobby_obj;
}
