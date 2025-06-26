using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject flowerjelly;

    public float moveduration = 0.2f;
    public float interval = 10f;    //주기
    public float destination = 8f;

    private Vector2 StartPos;
    private Vector2 TargetPos;
    public bool isMoving = false;

    private PetController controller;

    private void Start()
    {
        controller = GetComponent<PetController>();

        StartCoroutine(GiveJelly());

    }

    private IEnumerator GiveJelly()
    {
        while(true)
        {
            if(GameManager.instance.isGameover)
            {
                yield break;
            }

            if(!isMoving)
            {
                yield return StartCoroutine(Move());
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator Move()
    {
        isMoving = true;

        // 펫 따라가기 잠깐 중지
        if (controller != null)
        {
            controller.SetStop(true);
        }

        // 현재 위치 기준으로 타겟 위치 다시 계산
        StartPos = transform.position;
        TargetPos = StartPos + Vector2.right * destination;

        // 앞으로 이동
        yield return StartCoroutine(MoveOverTime(StartPos, TargetPos, moveduration));
        GameObject obj = Instantiate(flowerjelly, TargetPos, Quaternion.identity);

        if(controller != null && controller.character != null)
        {
            Vector3 characterback = controller.character.position + controller.petPos;
            yield return StartCoroutine(MoveOverTime(TargetPos, characterback, moveduration));
        }
        //제자리로 
        if (controller != null)
        {
            controller.SetStop(false);
        }

        isMoving = false;
    }

    private IEnumerator MoveOverTime(Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;

        while(elapsed < duration)
        {
            Vector2 newpos = Vector2.Lerp(from, to, elapsed / duration);
            transform.position = new Vector3(newpos.x, newpos.y, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(to.x, to.y, transform.position.z);
    }
}
