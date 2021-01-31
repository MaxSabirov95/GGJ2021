using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovementX, float deltaMovementY);
    public ParallaxCameraDelegate onCameraTranslate;
    private Vector3 oldPosition;
    void Start()
    {
        oldPosition = transform.position;
    }
    void Update()
    {
        if (transform.position != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                float deltaX = oldPosition.x - transform.position.x;
                float deltaY = oldPosition.y - transform.position.y;
                onCameraTranslate(deltaX, deltaY);
            }
            oldPosition = transform.position;
        }
    }
}
