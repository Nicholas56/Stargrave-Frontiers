using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveSelectShip : MonoBehaviour {

    public bool isTurn;
    public bool isOverUI;
    UIController user;

    Grid grid;
    Pathfinding paths;
    public GameObject currentShip;
    public GameObject targetPoint;

    // Use this for initialization
    void Start ()
    {
        isTurn = true;
        currentShip = null;
        grid = GetComponent<Grid>();
        paths = GetComponent<Pathfinding>();
        user = FindObjectOfType<UIController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isTurn)
        {
            if (currentShip != null && Input.GetMouseButtonDown(1)) //If the ship is selected and a right mouse click is detected
            {
                grid.CreateGrid();
                if (user.actNum == 0)
                {
                    paths.StartPosition = currentShip.transform; //Loads the selected ships position as the start position for the pathfinder
                    Instantiate(targetPoint, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity); //Creates a game object at the position of the mouse click
                    paths.TargetPosition = GameObject.FindGameObjectWithTag("Target").transform; //Loads the created game object as the target position for the pathfinder
                }
                else if (user.actNum == 1)
                {
                    currentShip.GetComponent<ShipControl>().FireWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }


            }

            if (grid.FinalPath != null)
            {
                currentShip.GetComponent<ShipControl>().pathToTake = grid.FinalPath;//Loads the path from the pathfinder into the ship control

                Destroy(GameObject.FindGameObjectWithTag("Target"));//After it is used, destroys the target point game object
                grid.FinalPath = null;
            }

            if (GameObject.FindGameObjectsWithTag("Target").Length > 1)//If more than one target point exists
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");//This finds them
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Target").Length; i++)
                {
                    Destroy(targets[i]);//And destroys them all
                }
            }

            
            if (currentShip!=null&&currentShip.GetComponent<ShipControl>().selectedMouse == false && Input.GetMouseButtonDown(0)) //If a ship is selected and a left mouse click is detected
            {
                Debug.Log("deselect");
                if (!isOverUI)
                {
                    currentShip.GetComponent<ShipControl>().isShipSelected = false;
                    currentShip = null;
                }
            }
            
        }
    }

    public void OverUI()
    {
        isOverUI = true;
    }

    public void ExitUI()
    {
        isOverUI = false;
    }
}
