using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinePlatform : MonoBehaviour
{
    public Action OnVineDestroyed;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            OnVineDestroyed();
            Destroy(this.gameObject);
        }
    }
}
