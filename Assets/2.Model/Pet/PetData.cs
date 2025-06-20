using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pet", menuName ="ScriptableObject/Pet_Data")]
public class PetData : ScriptableObject
{
    [Header("기본정보")]
    public string PetName;
    public GameObject Pet_obj;
    public GameObject Grade;
    //public int Level;

    [Header("인게임")]
    public GameObject ingame_obj;
}