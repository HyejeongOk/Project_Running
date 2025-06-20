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
            Debug.Log("선택된 캐릭터가 없습니다.");
            return;
        }

        // 선택된 캐릭터 가져오기
        GameObject charcter = Instantiate(ScrollManager.instance.selectpet.Pet_obj,
            PetSpawner.position, Quaternion.identity, PetSpawner);
    }
}
