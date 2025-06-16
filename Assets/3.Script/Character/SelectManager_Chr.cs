using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager_Chr : MonoBehaviour
{
    public List<CharacterData> characterDatas;
    public List<SelectCharacter> characterSlots;  // ĳ���� ����

    private SelectCharacter selectCharacter = null;

    [Header("Scene�� ��ġ�� ĳ����")]
    public CharacterData selectedCharacterData; // ���� ���õ� ĳ����
    public Transform sceneCharacterSpawner; // ���� ĳ���� ��ġ
    public GameObject currentCharacter; // ���� ���� �ִ� ĳ����

    private void Start()
    {
        for(int i = 0; i < characterDatas.Count; i++)
        {
            characterSlots[i].characterdata = characterDatas[i];
            characterSlots[i].Initialized(this);
        }

        // �⺻ ĳ���� ���� �� �� ����
        selectedCharacterData = characterDatas[0];
        GameManager.instance.selectcharacter = selectedCharacterData;

        DisplayonSceneCharacter();

        // ������ ĳ���� ����
        SelectDisplayedCharacter();

    }

    public void SelectCharacter(SelectCharacter select)
    {
        // ���� ���õ� ĳ���� Idle ���·� ��ȯ
        if(selectCharacter != null)
        {
            selectCharacter.SetIdleState();
        }
        selectCharacter = select;
        selectCharacter.SetRunState();

        // ������ ĳ���� ������ ����
        selectedCharacterData = select.characterdata;
        GameManager.instance.selectcharacter = select.characterdata;

        // Scene�� ĳ���� �ݿ�
        DisplayonSceneCharacter();

        SelectDisplayedCharacter();
    }


    // ���� ĳ���� ��ġ
    public void DisplayonSceneCharacter()
    {
        //���� ĳ���� ����
        if(currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        //���ο� ĳ���� ����
        currentCharacter = Instantiate(selectedCharacterData.stand_obj, sceneCharacterSpawner.position, Quaternion.identity, sceneCharacterSpawner);
    }

    // ���� �ִ� ĳ���ʹ� ���õ� ĳ���Ͱ� �Ǿ�� �Ѵ�
    public void SelectDisplayedCharacter()
    {
        foreach(var slot in characterSlots)
        {
            if(slot.characterdata == selectedCharacterData)
            {
                slot.SetRunState(); // �޸��� ���
            }
            else
            {
                slot.SetIdleState();
            }
        }
    }
}
