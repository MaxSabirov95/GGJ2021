using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGhostAbilityPicked;
    public bool isGhost;

    private void Start()
    {
        BlackBoard.gameManager = this;
    }
}
