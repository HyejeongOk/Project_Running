using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public int score = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("�浹");
            GameManager.instance.AddScore(score);
            Debug.Log($"���� ȹ�� : {score}");
            Destroy(gameObject);
        }
    }
}
