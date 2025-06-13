using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public void PlayBtn()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
