using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Transform character; // 따라갈 캐릭터
    private Vector3 petPos;  // 캐릭터로부터 떨어진 거리 (초기 생성 위치)
    public Vector3 targetPos;   // 목적지

    private Animator animator;

    private bool isStop;

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if(character == null || isStop)
        {
            return;
        }

        targetPos = character.position + petPos;

        transform.position = new Vector3(targetPos.x,
            targetPos.y,
            transform.position.z);
    }

    public void SetTarget(Transform target, Vector3 Spawnpos)
    {
        character = target;

        // 캐릭터와 펫 스폰 위치 거리 계산
        petPos = Spawnpos - target.position;
    }

    public void SetStop(bool stop)
    {
        isStop = stop;

        if (stop)
        {
            animator.enabled = false;   // 애니메이션 정지
        }

        else
        {
            animator.enabled = true;    // 애니메이션 다시 실행
        }
    }
}
