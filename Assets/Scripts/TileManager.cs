using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject player;

    public SpriteRenderer sprite;

    public GameManager gameManager;

    public LayerMask layerMask;

    Color yellowHighlightSoft = new Color(1f, 1f, 0.8f, 0.2f);

    Color yellowHighlightHard = new Color(1f, 0.92f, 0.016f, 0.2f);

    Color attackHighlight = new Color(1f, 0f, 0f, 0.2f);

    public bool isSelected = false;
    bool isAttacking = false;

    public bool isSticks;

    // Use this for initialization
    void Start () {
        sprite = transform.GetComponent<SpriteRenderer>();

        player = GameObject.Find("Player");

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
    public void HighlightSoft()
    {
       // if (!isSelected)
            sprite.color = yellowHighlightSoft;

        isAttacking = false;
        isSelected = false;
    }

    public void CheckForAttack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 0.01f);

        foreach (Collider2D col in cols)
        {
            if (col.tag == "Huntable")
            {
                HighlightAttack();
            } else
            {
                //Do Nothing
            }
        }
    }

    public void HighlightAttack()
    {
        sprite.color = attackHighlight;
        isAttacking = true;

        isSelected = false;
    }

    public void HighlightHard()
    {
        if (!isAttacking)
        {
            sprite.color = yellowHighlightHard;
            isAttacking = false;

            isSelected = true;
        }
    }

    public void Unhighlight()
    {
       // if (isSelected)
            sprite.color = Color.clear;

        isAttacking = false;
        isSelected = false;
    }

    void OnMouseOver()
    {
        //   Collider2D checkForPlayer = Physics2D.OverlapCircle(transform.position, 0.01f, layerMask);

        //   if (checkForPlayer != null)
        //  {
        ///     if (player.GetComponent<TileSelector>().playerState == TileSelector.PlayerState.idle)
        ///     {
        ///         player.GetComponent<TileSelector>().playerState = TileSelector.PlayerState.highlighting;
        ///     }
        //   }

        //Debug.Log("help");

        if (Input.GetMouseButtonDown(0) && !isAttacking && isSelected)
        {
            player.GetComponent<TileSelector>().UpdateAfterMove();

            FindObjectOfType<AudioManager>().Play("PlayerMove");

            player.transform.position = transform.position;

            if (isSticks)
            {
                if (GameObject.FindGameObjectsWithTag("Player").Length > 1 && gameManager.huntablesStartMoving == false)
                {
                    gameManager.MoveAnimals();
                }

                gameManager.huntablesStartMoving = true;
            }

        } else if (Input.GetMouseButtonDown(0) && isAttacking)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 0.01f);

            foreach (Collider2D col in cols)
            {
                if (col.tag == "Huntable")
                {
                    FindObjectOfType<AudioManager>().Play("GunShot");
                    player.GetComponent<TileSelector>().canMove = false;
                    player.GetComponent<TileSelector>().UpdateAfterMove();

                    if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
                    {
                        player.GetComponent<TileSelector>().canMove = true;
                    }

                    if (gameManager.huntablesStartMoving == false)
                    {
                        Debug.Log("m");
                        gameManager.MoveAnimals();
                    }

                    Destroy(col.gameObject);
                    gameManager.huntablesStartMoving = true;
                }
            }
        }
    }

   // void OnMouseExit ()
  //  {
   //     if (player.GetComponent<TileSelector>().playerState == TileSelector.PlayerState.highlighting)
   //     {
    //        player.GetComponent<TileSelector>().playerState = TileSelector.PlayerState.idle;
   //     }
  //  }
}
