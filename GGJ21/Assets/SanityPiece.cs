using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityPiece : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //play victory sound, fade screen, then teleport back to tree
        }
    }
}
