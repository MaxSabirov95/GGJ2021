using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinePlatform : MonoBehaviour
{
    public static bool vineDestroyed;

    private void Start()
    {
        vineDestroyed = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            vineDestroyed = true;
            Destroy(this.gameObject);
        }
    }
}
