using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //���, �� �⺻�ӵ�
    public float basicbgSpeed = 5f;
    public float basicmapSpeed = 5f;

    //���, �� �ӵ�
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

            // UI �߰�
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