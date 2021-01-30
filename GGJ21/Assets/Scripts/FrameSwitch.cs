using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FrameSwitch : MonoBehaviour
{
    public GameObject activeLevel;
    public enum levels { safezone, level1, level2}
    public levels lvls;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            activeLevel.SetActive(true);
            switch(lvls)
            {
                case levels.safezone:
                    BlackBoard.gameManager.safezone = true;
                    break;
                case levels.level1:
                    BlackBoard.gameManager.level1 = true;
                    break;
                case levels.level2:
                    BlackBoard.gameManager.level2 = true;
                    break;
                default:
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            activeLevel.SetActive(false);
            switch (lvls)
            {
                case levels.safezone:
                    BlackBoard.gameManager.safezone = false;
                    break;
                case levels.level1:
                    BlackBoard.gameManager.level1 = false;
                    break;
                case levels.level2:
                    BlackBoard.gameManager.level2 = false;
                    break;
                default:
                    break;
            }
            BlackBoard.gameManager.Music();
        }
    }
}
