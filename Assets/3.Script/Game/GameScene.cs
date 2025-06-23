using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    public Text score_txt;

    public GameObject StopBtn;
    public HPBar hp;

    private PlayerController player;
    private PetController pet;
    private Map_Spawner map;

    private void Start()
    {
        GameManager.instance.InitializeScore(score_txt);

        // ���� ���ٸ� ���⼭ ã��
        if(player ==  null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if(pet == null)
        {
            pet = FindObjectOfType<PetController>();
        }

        if(map == null)
        {
            map = FindObjectOfType<Map_Spawner>();
        }
    }

    // ���� ��ư
    public void JumpBtn()
    {
        if(player != null)
        {
            player.OnJumpButton();
        }
    }

    public void SlideBtn()
    {
        if (player != null)
        {
            player.OnSlideButton();
        }
    }

    public void SildeBtnExit()
    {
        if(player != null)
        {
            player.OnSlideButtonExit();
        }
    }

    // ���� ��ư
    public void OnClickStopBtn()
    {
        StopBtn.SetActive(true);

        Time.timeScale = 0f;

        AudioListener.pause = true;

        hp.StopDecreaseHP();

        GameManager.instance.bgSpeed = 0f;
        GameManager.instance.mapSpeed = 0f;


        if(pet != null)
        {
            pet.SetStop(true);
        }

        if(map != null)
        {
            map.SetPause(true);
        }
    }

    public void OnClickContinueBtn()
    {
        StopBtn.SetActive(false);

        Time.timeScale = 1f;

        AudioListener.pause = false;

        hp.StartDecreaseHP();

        GameManager.instance.bgSpeed = 5f;
        GameManager.instance.mapSpeed = 5f;


        if (pet != null)
        {
            pet.SetStop(false);
        }

        if(map != null)
        {
            map.SetPause(false);
        }
    }

    // �׸��α� ��ư
    public void QuitBtn()
    {
        Time.timeScale = 1f;

        AudioListener.pause = false;

        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    // �ٽ��ϱ� ��ư
    public void AgainBtn()
    {
        Time.timeScale = 1f;

        AudioListener.pause = false;

        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

}
