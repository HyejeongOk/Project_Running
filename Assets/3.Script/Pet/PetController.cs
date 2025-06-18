using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Transform character; // ���� ĳ����
    private Vector3 petPos;  // ĳ���ͷκ��� ������ �Ÿ� (�ʱ� ���� ��ġ)

    private Animator animator;

    private bool isStop;

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

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
