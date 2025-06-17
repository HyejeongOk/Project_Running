using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public CharacterData selectcharacter;
    public PetData selectpet;
 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public Text Score_txt;
    public bool isGameover = false;

    public int Score = 0;

    private void Update()
    {
        //if(isGameover)
        //{
        //    SceneManager.LoadScene(4, LoadSceneMode.Single);
        //}
    }

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

            // UI Ãß°¡
            Score_txt.text = Score.ToString();
        }
    }
}
