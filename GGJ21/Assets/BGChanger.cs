using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BGChanger : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;
    public Texture2D targetBgSprite;
    private float blendFactor;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            backgroundRenderer.material.SetTexture("_TargetSprite", targetBgSprite);
            blendFactor = 0;
            BlendBG();
        }
    }

    void BlendBG()
    {
        DOTween.To(() => blendFactor, x => blendFactor = x, 1, 0.75f).OnUpdate(() => backgroundRenderer.material.SetFloat("_LerpValue", blendFactor)).
            OnComplete(() => backgroundRenderer.material.SetTexture("_CurrentTex", targetBgSprite));
    }
}
