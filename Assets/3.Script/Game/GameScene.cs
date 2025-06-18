using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        // 만약 없다면 여기서 찾기
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

    // 정지 버튼
    public void OnClickStopBtn()
    {
        StopBtn.SetActive(true);

        GameManager.instance.bgSpeed = 0f;
        GameManager.instance.mapSpeed = 0f;

        hp.StopDecreaseHP();


        if(player != null)
        {
            player.SetStop(true);
        }

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

        GameManager.instance.bgSpeed = 10f;
        GameManager.instance.mapSpeed = 10f;

        hp.StartDecreaseHP();

        if (player != null)
        {
            player.SetStop(false);
        }

        if (pet != null)
        {
            pet.SetStop(false);
        }

        if(map != null)
        {
            map.SetPause(false);
        }
    }
}
