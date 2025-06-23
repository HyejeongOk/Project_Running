using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // ���� ���� �Ŵ���
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

    // ����
    public void Pause()
    {
        Time.timeScale = 0f;    // ��� ���� �ð� ����
        AudioListener.pause = true; //���� ����
        isPause = true;

        // �ִϸ��̼� ����
        GameManager.instance.SetAnimationSpeed(0f);

        // ���� ����
        GameManager.instance.SetPhysicsPause(true);
    }

    // �����
    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPause = false;

        // �ִϸ��̼� �����
        GameManager.instance.SetAnimationSpeed(1f);

        // ���� �簳
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
