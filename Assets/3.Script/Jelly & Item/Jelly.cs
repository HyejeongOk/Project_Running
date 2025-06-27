using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public int score = 10;
    public AudioClip jellyClip;

    private bool iscollected = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(iscollected)
        {
            return;
        }

        if(collision.CompareTag("Player"))
        {
            iscollected = true;
            GameManager.instance.AddScore(score);
            Destroy(gameObject);
            SFX.SoundPlay(jellyClip);
        }
    }
}
