using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPetSpawner : MonoBehaviour
{
    public Transform PetSpawner;

    private void Start()
    {
        if (ScrollManager.instance.selectpet == null)
        {
            Debug.Log("���õ� ĳ���Ͱ� �����ϴ�.");
            return;
        }

        // ���õ� ĳ���� ��������
        GameObject charcter = Instantiate(ScrollManager.instance.selectpet.Pet_obj,
            PetSpawner.position, Quaternion.identity, PetSpawner);
    }
}
