using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    public Text score;

    private void Start()
    {
        resultScore();
    }
    public void resultScore()
    {
        int resultscore = GameManager.instance.Score;
        score.text = resultscore.ToString();
    }

    public void LobbyScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
