using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityTree : MonoBehaviour
{
    [SerializeField] private SanityPiece[] pieces;
    [SerializeField] private Sprite[] stageSprites;

    private SpriteRenderer treeRenderer;
    private int recoveryStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        treeRenderer = GetComponent<SpriteRenderer>();
        foreach (SanityPiece piece in pieces)
        {
            piece.OnPieceRetrieved += RestoreTree;
        }
    }

    void RestoreTree()
    {
        recoveryStage++;
        treeRenderer.sprite = stageSprites[recoveryStage - 1];
    }
}
