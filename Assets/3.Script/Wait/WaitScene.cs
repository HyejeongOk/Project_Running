using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitScene : MonoBehaviour
{
    [SerializeField] private AudioClip btnclip;

    public GameObject Character_btn;
    public GameObject Pet_btn;

    private SelectManager_Chr selectchr_mgr;
    private SelectManager_Pet selectpet_mgr;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ĳ���� ����â ����
    public void OpenCharacterSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Character_btn.SetActive(true);
    }

    // ĳ���� ����â ����
    public void CloseCharacterSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Character_btn.SetActive(false);
    }

    // �� ����â ����
    public void OpenPetSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Pet_btn.SetActive(true);
    }

    // �� ����â ����
    public void ClosePetSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Pet_btn.SetActive(false);
    }

    // �����ϱ�
    public void OnClickPlayBtn()
    {
        if(ScrollManager.instance.selectcharacter == null || ScrollManager.instance.selectpet == null)
        {
            Debug.Log("ĳ���Ͷ� ���� ��� ���õ��� �ʾҽ��ϴ�.");
        }

        audioSource.clip = btnclip;
        audioSource.Play();
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    // �κ�� ���ư���
    public void BacktoLobbyScene()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
