using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager_Pet : MonoBehaviour
{
    public List<PetData> Petdatas;
    public List<SelectPet> petSlots;  // 캐릭터 슬롯

    private SelectPet selectPet = null;

    [Header("Scene에 배치할 펫")]
    public PetData selectedPetData; // 현재 선택된 캐릭터
    public Transform scenePetSpawner; // 씬에 캐릭터 위치
    public GameObject currentPet; // 현재 씬에 있는 캐릭터

    private void Start()
    {
        for (int i = 0; i < Petdatas.Count; i++)
        {
            petSlots[i].petdata = Petdatas[i];
            petSlots[i].Initialized(this);
        }

        // 기본 캐릭터 선택 및 씬 생성
        selectedPetData = Petdatas[0];
        GameManager.instance.selectpet = selectedPetData;

        DisplayonScenePet();

        SelectDisplayedCharacter();
    }

    public void SelectPet(SelectPet select)
    {
        if (selectPet!= null)
        {
            selectPet.SetIdleState();
        }
        selectPet= select;
        selectPet.SetRunState();

        GameManager.instance.selectpet = select.petdata;
    }

    // 씬에 캐릭터 배치
    public void DisplayonScenePet()
    {
        //기존 캐릭터 삭제
        if (currentPet != null)
        {
            Destroy(currentPet);
        }

        //새로운 캐릭터 생성
        currentPet = Instantiate(selectedPetData.Pet_obj, scenePetSpawner.position, Quaternion.identity, scenePetSpawner);

        SelectDisplayedCharacter(); //버튼 여부
    }

    // 선택하면 체크 표시
    public void SelectDisplayedCharacter()
    {
        foreach (var slot in petSlots)
        {
            if (slot.petdata == selectedPetData)
            {
                slot.SetRunState(); // 체크된 모습
            }
            else
            {
                slot.SetIdleState();    // 체크 해제
            }
        }
    }
}
