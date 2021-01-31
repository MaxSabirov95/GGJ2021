using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ParallaxBackGround : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
    public float parallaxXSpeed = 1f;
    public float parallaxYSpeed = 1f;

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;
        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
                layer.parallaxFactorX *= parallaxXSpeed;
                layer.parallaxFactorY *= parallaxYSpeed;
            }
        }
    }
    void Move(float deltaX, float deltaY)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(deltaX, deltaY);
        }
    }
}
