using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Obj : MonoBehaviour
{
    public SpriteRenderer sprite;
    public AudioSource audioSource;
    public MapData mapData;

    private void Awake()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        mapData = ScrollManager.instance.selectmap;

        if (mapData != null)
        {
            sprite.sprite = mapData.Mapbg_inGame;

            if (mapData.Bgm != null)
            {
                audioSource.clip = mapData.Bgm;
                audioSource.Play();
            }
        }
    }

    private void Update()
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }

        mapData = ScrollManager.instance.selectmap;

        transform.Translate(Vector2.left * GameManager.instance.bgSpeed * Time.deltaTime);
    }
}
