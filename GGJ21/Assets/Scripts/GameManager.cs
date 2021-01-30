using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action<bool> ToggleGhost;

    bool isGhostAbilityPicked = true;
    bool isGhost;
    public bool level1;
    public bool level2;
    public bool safezone;

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

    public void Music()
    {
        BlackBoard.soundsManager.BackgroundMusic.Stop();
        if (safezone && !level1 && !level2)
        {
            BlackBoard.soundsManager.MusicList(0);
        }

        else if (!safezone && level1 && !level2)
        {
            BlackBoard.soundsManager.MusicList(2);
        }

        else if (!safezone && !level1 && level2)
        {
            BlackBoard.soundsManager.MusicList(2);
        }
    }
}
