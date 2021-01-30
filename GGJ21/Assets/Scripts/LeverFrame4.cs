using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFrame4 : MonoBehaviour
{
    public Animator animatorStatue;
    public Animator animatorLever;
    bool inRadius;
    private bool canBeInteracted = true;

    //void Start()
    //{
    //    BlackBoard.gameManager.ToggleGhost += LeverEnabled;
    //}

    void Update()
    {
        if (canBeInteracted && inRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(StatueAnimating());
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
