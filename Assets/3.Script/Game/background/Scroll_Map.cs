using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Map : MonoBehaviour
{
    [SerializeField] private float destroyXpos = -30f;

    // ±§º”¡˙¡÷ æ∆¿Ã≈€ »πµÊ Ω√

    private void Update()
    {
        if(GameManager.instance.isGameover)
        {
            return;
        }

        transform.Translate(Vector2.left * GameManager.instance.mapSpeed * Time.deltaTime);

        if(transform.position.x <= destroyXpos)
        {
            Destroy(gameObject);
        }
    }
}
