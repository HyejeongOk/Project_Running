using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public CharacterData characterdata;
    public Transform waitingSpawner;    // �������� ĳ���� ��ġ
    public Transform spawner;   // ĳ���� ����â���� ���� ��ġ

    [Header("�����ϱ� ��ư")]
    public Image select_btn; // �����ϱ�
    public Image check_btn; // üũǥ��

    [Header("ĳ���� ����â")]
    public Text name_txt;
    private GameObject stand_obj;
    private GameObject run_obj;
    private GameObject grade;

    private SelectManager_Chr selectchr_mgr;

    public void Initialized(SelectManager_Chr mgr)
    {
        selectchr_mgr = mgr;

        //������ ����
        grade = Instantiate(characterdata.Grade, spawner.position + new Vector3(0, -165f, 0f), Quaternion.identity, spawner);
        stand_obj = Instantiate(characterdata.stand_obj, spawner.position, Quaternion.identity, spawner);
        run_obj = Instantiate(characterdata.run_obj, spawner.position + new Vector3(0f, 45f, 0f), Quaternion.identity, spawner);

        stand_obj.SetActive(true);
        run_obj.SetActive(false);

        check_btn.gameObject.SetActive(false);

        name_txt.text = characterdata.CharacterName;
    }

    public void OnSelectButtonClicked()
    {
        selectchr_mgr.SelectCharacter(this);

    }

    public void SetIdleState()
    {
        stand_obj.SetActive(true);
        run_obj.SetActive(false);
        select_btn.gameObject.SetActive(true);
        check_btn.gameObject.SetActive(false);
    }

    public void SetRunState()
    {
        stand_obj.SetActive(false);
        run_obj.SetActive(true);
        select_btn.gameObject.SetActive(false);
        check_btn.gameObject.SetActive(true);
    }

}
