using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitScene : MonoBehaviour
{
    public void BacktoLobbyScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
