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

    // 캐릭터 선택창 열기
    public void OpenCharacterSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Character_btn.SetActive(true);
    }

    // 캐릭터 선택창 끄기
    public void CloseCharacterSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Character_btn.SetActive(false);
    }

    // 펫 선택창 열기
    public void OpenPetSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Pet_btn.SetActive(true);
    }

    // 펫 선택창 끄기
    public void ClosePetSelect()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        Pet_btn.SetActive(false);
    }

    // 게임하기
    public void OnClickPlayBtn()
    {
        if(ScrollManager.instance.selectcharacter == null || ScrollManager.instance.selectpet == null)
        {
            Debug.Log("캐릭터랑 펫이 모두 선택되지 않았습니다.");
        }

        audioSource.clip = btnclip;
        audioSource.Play();
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    // 로비로 돌아가기
    public void BacktoLobbyScene()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
