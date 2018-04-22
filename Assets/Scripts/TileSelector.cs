using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour {

    public LayerMask layerMaskFill;

    public GameManager gameManager;

    float minDepth = -Mathf.Infinity;
    float maxDepth = Mathf.Infinity;

    public enum PlayerState { highlighting, selecting, unhighlighting, idle };

    public PlayerState playerState = PlayerState.idle;

    public bool canMove = true;

    public bool horizontalShoot;
    public bool verticalShoot;
    public bool diagonalShoot;

    public int range;

    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update ()
    {
        switch (playerState)
        {
            case PlayerState.highlighting:
                {

                    Collider2D[] tilesColliding = Physics2D.OverlapCircleAll(transform.position, 0.7f, layerMaskFill, minDepth, maxDepth);

                    foreach (Collider2D tileColliding in tilesColliding)
                    {
                        tileColliding.GetComponent<TileManager>().HighlightSoft();
                    }

                    break;
                }

            case PlayerState.selecting:
                {
                    foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
                    {
                        tile.GetComponent<TileManager>().player = gameObject;
                    }

                    CheckAttack();

                    Collider2D[] tilesColliding = Physics2D.OverlapCircleAll(transform.position, 0.7f, layerMaskFill, minDepth, maxDepth);

                    foreach (Collider2D tileColliding in tilesColliding)
                    {
                        tileColliding.GetComponent<TileManager>().HighlightHard();
                    }

                    break;
                }

            case PlayerState.unhighlighting:
                {
                    Collider2D[] tilesColliding = Physics2D.OverlapCircleAll(transform.position, 10f, layerMaskFill, minDepth, maxDepth);

                    foreach (Collider2D tileColliding in tilesColliding)
                    {
                        tileColliding.GetComponent<TileManager>().Unhighlight();
                    }

                    playerState = PlayerState.idle;

                    break;
                }

            case PlayerState.idle:
                {
                    break;
                }

            default:
                {
                    break;
                }
        }

        if (playerState == PlayerState.selecting)
        {
            if (Input.GetMouseButtonDown(1))
            {
                playerState = PlayerState.unhighlighting;
            }
        }

        if (playerState == PlayerState.selecting)
        {
            if (Input.GetKeyDown(KeyCode.S) && canMove)
            {
                UpdateAfterMove();
            }
        }
    }

    void OnMouseOver()
    {
        if (playerState == PlayerState.idle && canMove)
        {
            playerState = PlayerState.highlighting;
        }

        if (playerState == PlayerState.highlighting)
        {
            if (Input.GetMouseButtonDown(0) && canMove)
            {
                playerState = PlayerState.selecting;
            }
        }
    }

    void OnMouseExit()
    {
        if (playerState == PlayerState.highlighting)
        {
            playerState = PlayerState.unhighlighting;
        }
    }

    public void UpdateAfterMove ()
    {
        Collider2D[] tilesColliding = Physics2D.OverlapCircleAll(transform.position, 10f, layerMaskFill, minDepth, maxDepth);

        foreach (Collider2D tileColliding in tilesColliding)
        {
            tileColliding.GetComponent<TileManager>().Unhighlight();
        }

        gameManager.objectsToMove.RemoveAt(0);
        canMove = false;
        playerState = PlayerState.idle;
    }

    void CheckAttack()
    {
        Collider2D[] tilesColliding = Physics2D.OverlapCircleAll(transform.position, 2f, layerMaskFill, minDepth, maxDepth);

        for (int i = 0; i < range + 1; i++)
        {
            foreach (Collider2D col in tilesColliding)
            {
                if (horizontalShoot)
                {
                    if (col.transform.position == new Vector3(transform.position.x + -0.5f * i, transform.position.y + 0.5f * i))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }

                    if (col.transform.position == new Vector3(transform.position.x + 0.5f * i, transform.position.y + -0.5f * i))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }
                }

                if (verticalShoot)
                {
                    if (col.transform.position == new Vector3(transform.position.x + 0.5f * i, transform.position.y + 0.5f * i))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }



                    if (col.transform.position == new Vector3(transform.position.x + -0.5f * i, transform.position.y + -0.5f * i))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }
                }

                if (diagonalShoot)
                {
                    if (col.transform.position == new Vector3(transform.position.x + 1f * i, transform.position.y))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }

                    if (col.transform.position == new Vector3(transform.position.x + -1f * i, transform.position.y))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }

                    if (col.transform.position == new Vector3(transform.position.x, transform.position.y + -1f * i))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }

                    if (col.transform.position == new Vector3(transform.position.x, transform.position.y + 1f * i))
                    {
                        col.GetComponent<TileManager>().CheckForAttack();
                    }
                }
            }
        }
    }
}
