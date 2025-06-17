using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitScene : MonoBehaviour
{
    public GameObject Character_btn;
    public GameObject Pet_btn;

    private SelectManager_Chr selectchr_mgr;
    private SelectManager_Pet selectpet_mgr;

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

    // �����ϱ�
    public void OnClickPlayBtn()
    {
        if(GameManager.instance.selectcharacter == null || GameManager.instance.selectpet == null)
        {
            Debug.Log("ĳ���Ͷ� ���� ��� ���õ��� �ʾҽ��ϴ�.");
        }

        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    // �κ�� ���ư���
    public void BacktoLobbyScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
