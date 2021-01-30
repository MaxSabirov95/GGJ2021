using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGhostAbilityPicked;
    public bool isGhost;
    public bool level1;
    public bool level2;
    public bool safezone;

    private void Start()
    {
        BlackBoard.gameManager = this;
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
