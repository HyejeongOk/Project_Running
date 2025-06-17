using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Map : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float destroyXpos = -30f;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(transform.position.x <= destroyXpos)
        {
            Destroy(gameObject);
        }
    }
}
