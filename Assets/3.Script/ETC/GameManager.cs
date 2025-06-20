using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //배경, 맵 기본속도
    public float basicbgSpeed = 5f;
    public float basicmapSpeed = 5f;

    //배경, 맵 속도
    public float bgSpeed = 5f;
    public float mapSpeed = 5f;
    
    public static GameManager instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public Text Score_txt;
    public bool isGameover = false;

    public int Score = 0;

    public void InitializeScore(Text text)
    {
        Score_txt = text;
        Score = 0;
    }
    
    public void AddScore(int s)
    {
        if(!isGameover)
        {
            Score += s;

            // UI 추가
            Score_txt.text = Score.ToString();
        }
    }

    public IEnumerator Gameover_co()
    {
        isGameover = true;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(4, LoadSceneMode.Single);

        
    }
}