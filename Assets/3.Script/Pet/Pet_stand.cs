using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pet_stand : MonoBehaviour
{
    private Tween tween;
    // Image  UI�� ������ �ִϸ��̼��� ������ ��
    // RectTransform�� �����´�
    // UI Image : Tween�� ����� �� DoAnchorPos
    // 3D ������Ʈ�� DoLocalMoveY�� ����.
   private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        tween = rect.DOAnchorPosY(rect.anchoredPosition.y - 45f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    // Tween ����ó��
    private void OnDestroy()
    {
        if(tween != null && tween.IsActive())
        {
            tween.Kill();   //DoTween Ʈ���� �����ϰ� ����
        }
    }
}
