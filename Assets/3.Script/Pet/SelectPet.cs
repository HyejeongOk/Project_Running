using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPet : MonoBehaviour
{
    [SerializeField] private AudioClip selectclip;

    public PetData petdata;
    public Transform spawner;   // ĳ���� ���� ��ġ

    [Header("�����ϱ� ��ư")]
    public Image select_btn; // �����ϱ�
    public Image check_btn; // üũǥ��

    [Header("ĳ����")]
    public Text name_txt;
    private GameObject pet_obj;
    private GameObject grade;
    public float GradeY = -165f;

    private SelectManager_Pet selectchr_mgr;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialized(SelectManager_Pet mgr)
    {
        selectchr_mgr = mgr;

        //������ ����
        grade = Instantiate(petdata.Grade, spawner.position + new Vector3(0, GradeY, 0f), Quaternion.identity, spawner);
        pet_obj = Instantiate(petdata.Pet_obj, spawner.position, Quaternion.identity, spawner);

        check_btn.gameObject.SetActive(false);

        name_txt.text = petdata.PetName;
    }

    public void OnSelectButtonClicked()
    {
        audioSource.clip = selectclip;
        audioSource.Play();

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
