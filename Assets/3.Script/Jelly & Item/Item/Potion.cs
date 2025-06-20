using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int amount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            HPBar hp = FindObjectOfType<HPBar>();

            if(hp != null)
            {
                hp.Heal(amount);

            }
            Destroy(gameObject);
        }
    }
}
