using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitScene : MonoBehaviour
{
    public GameObject Character_btn;
    public GameObject Pet_btn;

    // 캐릭터 선택창 열기
    public void OpenCharacterSelect()
    {
        Character_btn.SetActive(true);
    }

    // 캐릭터 선택창 끄기
    public void CloseCharacterSelect()
    {
        Character_btn.SetActive(false);
    }

    // 펫 선택창 열기
    public void OpenPetSelect()
    {
        Pet_btn.SetActive(true);
    }

    // 펫 선택창 끄기
    public void ClosePetSelect()
    {
        Pet_btn.SetActive(false);
    }


    // 로비로 돌아가기
    public void BacktoLobbyScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
