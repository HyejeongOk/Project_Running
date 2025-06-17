using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Text score_txt;

    private void Start()
    {
        GameManager.instance.InitializeScore(score_txt);
    }
    // 정지 버튼
}
