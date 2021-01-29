using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFrame4 : MonoBehaviour
{
    public GameObject gate;
    public bool inRadius;
    void Update()
    {
        if (BlackBoard.gameManager.isGhost && inRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gate);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inRadius = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inRadius = false;
        }
    }
}
