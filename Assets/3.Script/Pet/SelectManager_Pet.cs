using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager_Pet : MonoBehaviour
{
    public List<PetData> Petdatas;
    public List<SelectPet> petSlots;  // �� ����

    private SelectPet selectPet = null;

    [Header("Scene�� ��ġ�� ��")]
    public PetData selectedPetData; // ���� ���õ� ��
    public Transform scenePetSpawner; // ���� �� ��ġ
    public GameObject currentPet; // ���� ���� �ִ� ��

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

        // ������ �� ����
        SelectDisplayedPet();
    }

    public void SelectPet(SelectPet select)
    {
        if (selectPet!= null)
        {
            selectPet.SetIdleState();
        }
        selectPet= select;
        selectPet.SetRunState();

        selectedPetData = select.petdata;
        GameManager.instance.selectpet = select.petdata;

        DisplayonScenePet();

        // ������ �� ����
        SelectDisplayedPet();
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
    }

    // �����ϸ� üũ ǥ��
    public void SelectDisplayedPet()
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
