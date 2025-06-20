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
            Debug.Log("���õ� ĳ���Ͱ� �����ϴ�.");
            return;
        }

        // ���õ� ĳ���� ��������
        GameObject charcter = Instantiate(ScrollManager.instance.selectcharacter.run_obj,
            characterSpawner.position, Quaternion.identity, characterSpawner);
        charcter.SetActive(true);
    }
}
