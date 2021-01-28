using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.forward * 100 * rotateSpeed * Time.deltaTime);
    }
}
