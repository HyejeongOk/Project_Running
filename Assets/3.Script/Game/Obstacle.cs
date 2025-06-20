using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 5;
    private HPBar hp;

    private void Start()
    {
        hp = FindObjectOfType<HPBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hp.Damage(15);
        }
    }
}
