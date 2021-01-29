using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGhost : MonoBehaviour
{
    public SpriteRenderer ghostOn;
    public SpriteRenderer ghostOff;

    void Update()
    {
        ghostOn.gameObject.SetActive(BlackBoard.gameManager.isGhost ? true : false);
        ghostOff.gameObject.SetActive(BlackBoard.gameManager.isGhost ? false : true);
    }
}
