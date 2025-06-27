using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 5;
    private HPBar hp;
    private PlayerController player;

    private void Start()
    {
        hp = FindObjectOfType<HPBar>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.isGiant || player.isBlast)
            {
                Destroy(gameObject);
                return;
            }

            if(player.isInvincible)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                return;
            }

            hp.Damage(15);

        }
    }
}
