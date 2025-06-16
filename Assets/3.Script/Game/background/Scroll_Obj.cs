using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Obj : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
