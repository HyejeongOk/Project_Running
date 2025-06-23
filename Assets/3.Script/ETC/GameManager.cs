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

    //일시 정지 속도
    public float bgPause, mapPause = 0f;
    
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

    public void PauseGame()
    {
        bgPause = bgSpeed;
        mapPause = mapSpeed;

        bgSpeed = 0f;
        mapSpeed = 0f;
    }

    public void ResumeGame()
    {
        bgSpeed = bgPause;
        mapSpeed = mapPause;
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

    // 애니메이션
    public void SetAnimationSpeed(float speed)
    {
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach(Animator ani in animators)
        {
            ani.speed = speed;
        }
    }

    public void SetPhysicsPause(bool isPause)
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if(isPause)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            else
            {
                rb.isKinematic = false;
            }
        }
    }
}