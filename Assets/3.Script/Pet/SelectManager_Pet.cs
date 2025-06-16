using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager_Pet : MonoBehaviour
{
    public List<PetData> Petdatas;
    public List<SelectPet> petSlots;  // ĳ���� ����

    private SelectPet selectPet = null;

    [Header("Scene�� ��ġ�� ��")]
    public PetData selectedPetData; // ���� ���õ� ĳ����
    public Transform scenePetSpawner; // ���� ĳ���� ��ġ
    public GameObject currentPet; // ���� ���� �ִ� ĳ����

    private void Start()
    {
        for (int i = 0; i < Petdatas.Count; i++)
        {
            petSlots[i].petdata = Petdatas[i];
            petSlots[i].Initialized(this);
        }

        // �⺻ ĳ���� ���� �� �� ����
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

    // ���� ĳ���� ��ġ
    public void DisplayonScenePet()
    {
        //���� ĳ���� ����
        if (currentPet != null)
        {
            Destroy(currentPet);
        }

        //���ο� ĳ���� ����
        currentPet = Instantiate(selectedPetData.Pet_obj, scenePetSpawner.position, Quaternion.identity, scenePetSpawner);

        SelectDisplayedCharacter(); //��ư ����
    }

    // �����ϸ� üũ ǥ��
    public void SelectDisplayedCharacter()
    {
        foreach (var slot in petSlots)
        {
            if (slot.petdata == selectedPetData)
            {
                slot.SetRunState(); // üũ�� ���
            }
            else
            {
                slot.SetIdleState();    // üũ ����
            }
        }
    }
}
