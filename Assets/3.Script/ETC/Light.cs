using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Light : MonoBehaviour
{
    public Image light_img;


    private void OnEnable()
    {
        StartFlicker();
    }
    private void OnDisable()
    {
        light_img.DOKill(); // Dotween 애니메이션 종료
    }

    private void StartFlicker()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(light_img.DOFade(0.5f, 0.8f))    // 어두워짐
            .AppendInterval(Random.Range(0.3f, 1f))
            .Append(light_img.DOFade(1f, 0.8f)) //밝아짐
            .AppendInterval(Random.Range(0.3f, 1.5f))
            .SetLoops(-1);  // 무한반복
    }
}
