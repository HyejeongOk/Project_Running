using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Stand : MonoBehaviour
{
    public Image blink_img;
    private Coroutine BlinkCo;

    private void OnEnable()
    {
        BlinkCo = StartCoroutine(Blink_co());
    }

    private void OnDisable()
    {
        if(BlinkCo != null)
        {
            StopCoroutine(BlinkCo);
        }
    }

    private IEnumerator Blink_co()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            blink_img.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            blink_img.gameObject.SetActive(false);
        }

    }
}
