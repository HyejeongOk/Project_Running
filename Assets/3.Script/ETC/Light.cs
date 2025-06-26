using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Light : MonoBehaviour
{
    public Image light_img;
    private Sequence flickerseq;

    private void OnEnable()
    {
        StartFlicker();
    }
    private void OnDisable()
    {
        if(flickerseq != null && flickerseq.IsActive())
        {
            flickerseq.Kill();  // 시퀀스 강제 종료
            flickerseq = null;
        }
    }

    private void StartFlicker()
    {
        if(light_img == null)
        {
            return;
        }

        if(flickerseq != null && flickerseq.IsActive())
        {
            flickerseq.Kill();
        }

        flickerseq = DOTween.Sequence();
        flickerseq.Append(light_img.DOFade(0.5f, 0.8f))    // 어두워짐
            .AppendInterval(Random.Range(0.3f, 1f))
            .Append(light_img.DOFade(1f, 0.8f)) //밝아짐
            .AppendInterval(Random.Range(0.3f, 1.5f))
            .SetLoops(-1)  // 무한반복
            .SetId(light_img);  //안전하게 ID 지정
    }
}
