using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_loop : MonoBehaviour
{
    private float width;

    private void Start()
    {
        width = transform.GetComponent<BoxCollider2D>().size.x * 3.48f;
    }

    private void Update()
    {
        if(transform.position.x < -width)
        {
            Vector2 offset = new Vector2(width * 2f, 0f);
            transform.position = (Vector2) transform.position + offset;
        }
    }

}
