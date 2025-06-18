using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform characterSpawner;

    private void Start()
    {
        if(ScrollManager.instance.selectcharacter ==  null)
        {
            Debug.Log("���õ� ĳ���Ͱ� �����ϴ�.");
            return;
        }

        // ���õ� ĳ���� ��������
        GameObject charcter = Instantiate(ScrollManager.instance.selectcharacter.ingame_obj, 
            characterSpawner.position, Quaternion.identity, characterSpawner);
    }
}
