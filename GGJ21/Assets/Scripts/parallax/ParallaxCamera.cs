using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovementX, float deltaMovementY);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPosition;
    void Start()
    {
        oldPosition = transform.position.x;
    }
    void Update()
    {
        if (transform.position.x != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                float deltaX = oldPosition - transform.position.x;
                float deltaY = oldPosition - transform.position.y;
                onCameraTranslate(deltaX, deltaY);
            }
            oldPosition = transform.position.x;
        }
    }
}
