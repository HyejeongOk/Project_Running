using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public int score = 10;
    public AudioClip jellyClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.AddScore(score);
            Destroy(gameObject);
            SFX.SoundPlay(jellyClip);
        }
    }
}
