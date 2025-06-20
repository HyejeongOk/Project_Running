using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCookieSpawner : MonoBehaviour
{
    public Transform characterSpawner;

    private void Start()
    {
        if (ScrollManager.instance.selectcharacter == null)
        {
            Debug.Log("선택된 캐릭터가 없습니다.");
            return;
        }

        // 선택된 캐릭터 가져오기
        GameObject charcter = Instantiate(ScrollManager.instance.selectcharacter.run_obj,
            characterSpawner.position, Quaternion.identity, characterSpawner);
        charcter.SetActive(true);
    }
}
