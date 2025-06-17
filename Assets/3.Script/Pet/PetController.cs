using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Transform character; // 따라갈 캐릭터
    private Vector3 petPos;  // 캐릭터로부터 떨어진 거리 (초기 생성 위치)

    private void LateUpdate()
    {
        if(character == null)
        {
            return;
        }

        transform.position = character.position + petPos;
    }

    public void SetTarget(Transform target, Vector3 Spawnpos)
    {
        character = target;

        // 캐릭터와 펫 스폰 위치 거리 계산
        petPos = Spawnpos - target.position;
    }
}
