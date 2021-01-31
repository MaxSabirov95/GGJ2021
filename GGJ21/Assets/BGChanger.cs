using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LWRP;

public class BGChanger : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;
    public Sprite targetBgSprite;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            backgroundRenderer.sprite = targetBgSprite;
        }
    }
}
