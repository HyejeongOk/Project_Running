using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Stand : MonoBehaviour
{
    public Image blink_img;

    private void Start()
    {
        StartCoroutine(Blink_co());
    }

    //public void StartBlink()
    //{
    //    if(blink_co == null)
    //    {
    //        blink_co = StartCoroutine(Blink_co());
    //    }
    //}

    //public void StopBlink()
    //{
    //    if(blink_co != null)
    //    {
    //        StopCoroutine(blink_co);
    //        blink_co = null;
    //    }
    //    blink_img.gameObject.SetActive(false);  // ´«±ôºýÀÓ ²¨ÁÖ±â
    //}

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
