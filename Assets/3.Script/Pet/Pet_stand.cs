using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pet_stand : MonoBehaviour
{
    // Image  UI에 움직임 애니메이션을 구현할 때
    // RectTransform을 가져온다
    // UI Image : Tween을 사용할 때 DoAnchorPos
    // 3D 오브젝트는 DoLocalMoveY를 쓴다.
   private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.DOAnchorPosY(rect.anchoredPosition.y - 45f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
