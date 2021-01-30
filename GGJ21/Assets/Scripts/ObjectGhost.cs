using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGhost : MonoBehaviour
{
    public GameObject ghostOn;
    public GameObject ghostOff;
    public bool reversed;

    void Start()
    {
        BlackBoard.gameManager.ToggleGhost += TogglePlatform;
    }

    void TogglePlatform(bool ghost)
    {
        if (reversed)
        {
            ghost = !ghost;
        }
        ghostOn.SetActive(ghost);
        ghostOff.SetActive(!ghost);
        gameObject.layer = ghost ? 9 : 10;
    }
}
