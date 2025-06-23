using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // 게임 정지 매니저
    public static PauseManager instance = null;
    private bool isPause = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggelPause()
    {
        if(isPause)
        {
            Resume();
        }

        else
        {
            Pause();
        }
    }

    // 정지
    public void Pause()
    {
        Time.timeScale = 0f;    // 모든 게임 시간 멈춤
        AudioListener.pause = true; //사운드 정지
        isPause = true;

        // 애니메이션 멈춤
        GameManager.instance.SetAnimationSpeed(0f);

        // 물리 멈춤
        GameManager.instance.SetPhysicsPause(true);
    }

    // 재시작
    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPause = false;

        // 애니메이션 재시작
        GameManager.instance.SetAnimationSpeed(1f);

        // 물리 재개
        GameManager.instance.SetPhysicsPause(false);
    }

    public bool IsPaused()
    {
        return isPause;
    }

    public void OnPauseButton()
    {
        Pause();
        GameManager.instance.PauseGame();
    }

    public void OnResumeButton()
    {
        Resume();
        GameManager.instance.ResumeGame();
    }
}
