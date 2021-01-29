using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    bool isTriggered;
    public int SoundNum;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!isTriggered)
            {
                isTriggered = true;
                BlackBoard.soundsManager.SoundsList(SoundNum);
            }
        }
    }
}
