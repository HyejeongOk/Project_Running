using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    public Image Ok_btn; 

    public void LobbyScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
