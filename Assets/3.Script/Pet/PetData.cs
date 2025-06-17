using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pet", menuName ="ScriptableObject/Pet_Data")]
public class PetData : ScriptableObject
{
    [Header("�⺻����")]
    public string PetName;
    public GameObject Pet_obj;
    public GameObject Grade;
    //public int Level;

    [Header("�ΰ���")]
    public GameObject ingame_obj;
}