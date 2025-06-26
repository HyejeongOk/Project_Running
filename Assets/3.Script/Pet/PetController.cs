using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Transform character; // ���� ĳ����
    public Vector3 petPos;  // ĳ���ͷκ��� ������ �Ÿ� (�ʱ� ���� ��ġ)
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

        Debug.Log(petPos);
        targetPos = character.position + petPos;

        float speed = 5f;
        transform.position = Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime);
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
