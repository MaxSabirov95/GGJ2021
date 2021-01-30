using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFrame4 : MonoBehaviour
{
<<<<<<< HEAD
    //public Animator animtorStatue;
    public Animator animtorLever;
    public bool inRadius;
=======
    public Animator animatorStatue;
    public Animator animatorLever;
    bool inRadius;
    private bool canBeInteracted = true;

    //void Start()
    //{
    //    BlackBoard.gameManager.ToggleGhost += LeverEnabled;
    //}
>>>>>>> parent of 21ac223... Revert "Level 2"

    void Update()
    {
        if (canBeInteracted && inRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
<<<<<<< HEAD
                animtorLever.SetTrigger("push");
                //animtorStatue.SetTrigger("move");
=======
                StartCoroutine(StatueAnimating());
>>>>>>> parent of 21ac223... Revert "Level 2"
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

    void LeverEnabled(bool value)
    {
        canBeInteracted = value;
    }

    IEnumerator StatueAnimating()
    {
        Player.instance.enabled = false;
        animatorLever.SetTrigger("push");
        yield return new WaitForSeconds(1.1f);
        CameraController.instance.FocusOnPoint(animatorStatue.transform.position - Vector3.forward * 100f, false);

        yield return new WaitForSeconds(1.7f);
        animatorStatue.SetTrigger("move");
        yield return new WaitForSeconds(2.1f);
        CameraController.instance.FocusOnPoint(Player.instance.transform.position, true);
        Player.instance.enabled = true;
    }
}
