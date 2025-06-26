using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public Transform petSpawner;

    private void Start()
    {
        if(ScrollManager.instance.selectpet == null)
        {
            Debug.Log("���õ� ���� �����ϴ�.");
        }
        GameObject pet = Instantiate(ScrollManager.instance.selectpet.ingame_obj,
            petSpawner.position, Quaternion.identity);

        //Player �±װ� ���� ĳ���� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            PetController petcontrol = pet.GetComponent<PetController>();
            if(petcontrol != null)
            {
                // ĳ���Ϳ� petspawner�� ��� �Ÿ� ����
                petcontrol.SetTarget(player.transform, petSpawner.position);
            }
        }

        else
        {
            Debug.Log("Player�� ã�� �� �����ϴ�");
        }
    }
}
