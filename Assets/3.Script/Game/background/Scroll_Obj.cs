using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Obj : MonoBehaviour
{
    private void Update()
    {
        if(GameManager.instance.isGameover)
        {
            return;
        }

        transform.Translate(Vector2.left * GameManager.instance.bgSpeed * Time.deltaTime);
    }
}
