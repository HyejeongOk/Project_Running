using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    public float duration = 5f;
    public AudioClip GiantClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ActiveGiant(duration);

            Destroy(gameObject);
            SFX.SoundPlay(GiantClip);
        }
    }


}
