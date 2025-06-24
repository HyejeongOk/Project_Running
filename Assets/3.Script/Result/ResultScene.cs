using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    private AudioSource audioSource;
    public Text score;
    [SerializeField] AudioClip btnclip;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        resultScore();
    }
    public void resultScore()
    {
        int resultscore = GameManager.instance.Score;
        score.text = resultscore.ToString();
    }

    public void LobbyScene()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
