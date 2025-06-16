using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPet : MonoBehaviour
{
    public PetData petdata;
    public Transform spawner;   // 캐릭터 생성 위치

    [Header("선택하기 버튼")]
    public Image select_btn; // 선택하기
    public Image check_btn; // 체크표시

    [Header("캐릭터")]
    public Text name_txt;
    private GameObject pet_obj;
    private GameObject grade;

    private SelectManager_Pet selectchr_mgr;

    public void Initialized(SelectManager_Pet mgr)
    {
        selectchr_mgr = mgr;

        //프리팹 생성
        grade = Instantiate(petdata.Grade, spawner.position + new Vector3(0, -165f, 0f), Quaternion.identity, spawner);
        pet_obj = Instantiate(petdata.Pet_obj, spawner.position, Quaternion.identity, spawner);

        check_btn.gameObject.SetActive(false);

        name_txt.text = petdata.PetName;
    }

    public void OnSelectButtonClicked()
    {
        selectchr_mgr.SelectPet(this);
    }

    public void SetIdleState()
    {
        select_btn.gameObject.SetActive(true);
        check_btn.gameObject.SetActive(false);
    }

    public void SetRunState()
    {
        select_btn.gameObject.SetActive(false);
        check_btn.gameObject.SetActive(true);
    }
}
