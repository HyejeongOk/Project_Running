using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName ="ScriptableObject/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string CharacterName;
    public GameObject stand_obj;
    public GameObject run_obj;
    public GameObject Grade;
}
