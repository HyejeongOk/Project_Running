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
            Debug.Log("Ãæµ¹");
            GameManager.instance.AddScore(score);
            Debug.Log($"Á¡¼ö È¹µæ : {score}");
            Destroy(gameObject);
        }
    }
}
