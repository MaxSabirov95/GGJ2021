using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action<bool> ToggleGhost;

    bool isGhostAbilityPicked = true;
    bool isGhost;

    private void Awake()
    {
        BlackBoard.gameManager = this;
    }

    public void ToggleGhostStatus()
    {
        if (!isGhostAbilityPicked) return;
        isGhost = !isGhost;
        ToggleGhost(isGhost);
    }

    public void SetGhostAbilityPicked(bool value)
    {
        isGhostAbilityPicked = value;
    }

    public bool GetGhostAbilityPicked()
    {
        return isGhostAbilityPicked;
    }
}
