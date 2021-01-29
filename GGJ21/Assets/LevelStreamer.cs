using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStreamer : MonoBehaviour
{
    [SerializeField] private GameObject streamedLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            streamedLevel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            streamedLevel.SetActive(false);
        }
    }
}
