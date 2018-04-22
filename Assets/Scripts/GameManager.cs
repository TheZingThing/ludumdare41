using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<GameObject> objectsToMove = new List<GameObject>();

    public bool huntablesStartMoving = false;

    public int turnsLeft;
	
	// Update is called once per frame
	void Update () {

        if (objectsToMove.Count <= 0)
        {
            StartNewTurn();
        }

        if (GameObject.FindGameObjectsWithTag("Huntable").Length <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (turnsLeft <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    void StartNewTurn ()
    {
        turnsLeft--;

        Debug.Log("Starting a new turn");

        if (huntablesStartMoving)
        {
            foreach (GameObject objectToMove in GameObject.FindGameObjectsWithTag("Huntable"))
            {

                objectsToMove.Add(objectToMove);

                objectToMove.GetComponent<Huntable>().canMove = true;
                Debug.Log("Huntables can move");
            }
        }

        foreach (GameObject objectToMove in GameObject.FindGameObjectsWithTag("Player")) {

            objectsToMove.Add(objectToMove);

            objectToMove.GetComponent<TileSelector>().canMove = true;
        }

    }

    public void MoveAnimals ()
    {
        if (objectsToMove.Count > 0)
        {
            foreach (GameObject objectToMove in GameObject.FindGameObjectsWithTag("Huntable"))
            {
                objectsToMove.Add(objectToMove);

                objectToMove.GetComponent<Huntable>().canMove = true;
            }
        }
    }
}