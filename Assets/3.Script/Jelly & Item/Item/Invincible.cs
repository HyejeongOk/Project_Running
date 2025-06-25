using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    public float duration = 1f;
    public AudioClip BlastClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float bgSpeed = 15f;
            float mapSpeed = 15f;
            Debug.Log("±§º”¡˙¡÷ »πµÊ");
            collision.GetComponent<PlayerController>().ActiveBlast(duration, bgSpeed, mapSpeed);

            Destroy(gameObject);
            SFX.SoundPlay(BlastClip);
        }
    }
}
