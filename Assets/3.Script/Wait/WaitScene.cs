using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitScene : MonoBehaviour
{
    public GameObject Character_btn;
    public GameObject Pet_btn;

    // ĳ���� ����â ����
    public void OpenCharacterSelect()
    {
        Character_btn.SetActive(true);
    }

    // ĳ���� ����â ����
    public void CloseCharacterSelect()
    {
        Character_btn.SetActive(false);
    }

    // �� ����â ����
    public void OpenPetSelect()
    {
        Pet_btn.SetActive(true);
    }

    // �� ����â ����
    public void ClosePetSelect()
    {
        Pet_btn.SetActive(false);
    }


    // �κ�� ���ư���
    public void BacktoLobbyScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
