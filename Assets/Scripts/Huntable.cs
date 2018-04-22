using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Huntable : MonoBehaviour {

    public bool canMove = false;

    public GameManager gameManager;

    public List<string> moveList = new List<string>();

	// Use this for initialization
	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if (moveList.Count <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (gameManager.huntablesStartMoving)
        {
            if (canMove && moveList.Count > 0)
            {
                switch (moveList[0])
                {
                    case "up":
                        {
                            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f);
                            break;
                        }

                    case "down":
                        {
                            transform.position = new Vector3(transform.position.x + -0.5f, transform.position.y + -0.5f);
                            break;
                        }

                    case "left":
                        {
                            transform.position = new Vector3(transform.position.x + -0.5f, transform.position.y + 0.5f);
                            break;
                        }

                    case "right":
                        {
                            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + -0.5f);
                            break;
                        }
                }
                gameManager.objectsToMove.RemoveAt(0);
                moveList.RemoveAt(0);
                canMove = false;
            }
        }
	}

    //void OnDestroy()
    //{
    //    if (gameManager.huntablesStartMoving == true)
    //    {
    //        gameManager.objectsToMove.RemoveAt(0);
    //    }
    //}
}
