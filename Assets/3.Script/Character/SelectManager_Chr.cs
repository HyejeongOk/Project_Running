using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager_Chr : MonoBehaviour
{
    public List<CharacterData> characterDatas;
    public List<SelectCharacter> characterSlots;  // 캐릭터 슬롯

    private SelectCharacter selectCharacter = null;

    [Header("Scene에 배치할 캐릭터")]
    public CharacterData selectedCharacterData; // 현재 선택된 캐릭터
    public Transform sceneCharacterSpawner; // 씬에 캐릭터 위치
    public GameObject currentCharacter; // 현재 씬에 있는 캐릭터

    private void Start()
    {
        for(int i = 0; i < characterDatas.Count; i++)
        {
            characterSlots[i].characterdata = characterDatas[i];
            characterSlots[i].Initialized(this);
        }

        // 기본 캐릭터 선택 및 씬 생성
        selectedCharacterData = characterDatas[0];
        GameManager.instance.selectcharacter = selectedCharacterData;

        DisplayonSceneCharacter();

        // 선택한 캐릭터 갱신
        SelectDisplayedCharacter();

    }

    public void SelectCharacter(SelectCharacter select)
    {
        // 이전 선택된 캐릭터 Idle 상태로 전환
        if(selectCharacter != null)
        {
            selectCharacter.SetIdleState();
        }
        selectCharacter = select;
        selectCharacter.SetRunState();

        // 선택한 캐릭터 데이터 저장
        selectedCharacterData = select.characterdata;
        GameManager.instance.selectcharacter = select.characterdata;

        // Scene에 캐릭터 반영
        DisplayonSceneCharacter();

        SelectDisplayedCharacter();
    }


    // 씬에 캐릭터 배치
    public void DisplayonSceneCharacter()
    {
        //기존 캐릭터 삭제
        if(currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        //새로운 캐릭터 생성
        currentCharacter = Instantiate(selectedCharacterData.stand_obj, sceneCharacterSpawner.position, Quaternion.identity, sceneCharacterSpawner);
    }

    // 씬에 있는 캐릭터는 선택된 캐릭터가 되어야 한다
    public void SelectDisplayedCharacter()
    {
        foreach(var slot in characterSlots)
        {
            if(slot.characterdata == selectedCharacterData)
            {
                slot.SetRunState(); // 달리기 모습
            }
            else
            {
                slot.SetIdleState();
            }
        }
    }
}
