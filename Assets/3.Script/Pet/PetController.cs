using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Transform character; // ���� ĳ����
    private Vector3 petPos;  // ĳ���ͷκ��� ������ �Ÿ� (�ʱ� ���� ��ġ)
    public Vector3 targetPos;   // ������

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

        // ĳ���Ϳ� �� ���� ��ġ �Ÿ� ���
        petPos = Spawnpos - target.position;
    }

    public void SetStop(bool stop)
    {
        isStop = stop;

        if (stop)
        {
            animator.enabled = false;   // �ִϸ��̼� ����
        }

        else
        {
            animator.enabled = true;    // �ִϸ��̼� �ٽ� ����
        }
    }
}
