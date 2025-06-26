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
            Debug.Log("선택된 펫이 없습니다.");
        }
        GameObject pet = Instantiate(ScrollManager.instance.selectpet.ingame_obj,
            petSpawner.position, Quaternion.identity);

        //Player 태그가 붙은 캐릭터 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            PetController petcontrol = pet.GetComponent<PetController>();
            if(petcontrol != null)
            {
                // 캐릭터와 petspawner의 상대 거리 저장
                petcontrol.SetTarget(player.transform, petSpawner.position);
            }
        }

        else
        {
            Debug.Log("Player를 찾을 수 없습니다");
        }
    }
}
