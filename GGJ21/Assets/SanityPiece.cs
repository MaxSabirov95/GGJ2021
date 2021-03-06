﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SanityPiece : MonoBehaviour
{
    public Action OnPieceRetrieved;

    [SerializeField]
    private Transform teleportPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //play victory sound, fade screen, then teleport back to tree
            try
            {
                BlackBoard.soundsManager.SoundsList(13);
                OnPieceRetrieved();
                Player.instance.transform.position = teleportPosition.position;
                CameraController.instance.transform.position = teleportPosition.position;
                Destroy(gameObject,1f);
            }
            catch (Exception e)
            {
                SceneManager.LoadScene("Thanks For Playing");
            }
        }
    }
}
