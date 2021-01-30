using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGhost : MonoBehaviour
{
    public SpriteRenderer ghostOn;
    public SpriteRenderer ghostOff;

    void Update()
    {
        ghostOn.enabled = (BlackBoard.gameManager.isGhost ? true : false);
        ghostOff.enabled = (BlackBoard.gameManager.isGhost ? false : true);
    }
}
