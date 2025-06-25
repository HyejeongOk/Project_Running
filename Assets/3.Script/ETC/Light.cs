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
        light_img.DOKill(); // Dotween �ִϸ��̼� ����
    }

    private void StartFlicker()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(light_img.DOFade(0.5f, 0.8f))    // ��ο���
            .AppendInterval(Random.Range(0.3f, 1f))
            .Append(light_img.DOFade(1f, 0.8f)) //�����
            .AppendInterval(Random.Range(0.3f, 1.5f))
            .SetLoops(-1);  // ���ѹݺ�
    }
}
