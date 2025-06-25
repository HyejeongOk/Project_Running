using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pet_stand : MonoBehaviour
{
    private Tween tween;
    // Image  UI에 움직임 애니메이션을 구현할 때
    // RectTransform을 가져온다
    // UI Image : Tween을 사용할 때 DoAnchorPos
    // 3D 오브젝트는 DoLocalMoveY를 쓴다.
   private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        tween = rect.DOAnchorPosY(rect.anchoredPosition.y - 45f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    // Tween 예외처리
    private void OnDestroy()
    {
        if(tween != null && tween.IsActive())
        {
            tween.Kill();   //DoTween 트윈을 안전하게 종료
        }
    }
}
