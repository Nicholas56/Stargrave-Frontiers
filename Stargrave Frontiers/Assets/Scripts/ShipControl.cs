using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipControl : Ship
{
    public bool isShipSelected;
    public bool isShipMoving;

    public int shipId;

    public bool canMove;
    public float speed;
    public float waypointProximity;    

    MoveSelectShip selector;
    UIController user;
    public List<Node> pathToTake;
    Vector2 previousPosition;

    public bool selectedMouse;



    
    // Use this for initialization
    void Start ()
    {
        selector = FindObjectOfType<MoveSelectShip>();
        user = FindObjectOfType<UIController>();
        pathToTake = new List<Node>();
        canMove = true;

        actionPoints = 310;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (pathToTake.Count != 0 && actionPoints>0 && canMove)
        {
            isShipMoving = true;
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, pathToTake[0].Position, step);//The ship moves towards the location of the tile in the path
            if (Vector2.Distance(transform.position, pathToTake[0].Position) < waypointProximity)//When the ship is close enough to the tile, the waypoint is changed
            {
                previousPosition = pathToTake[0].Position;//stores the current position as the previous one
                pathToTake.RemoveAt(0);//Changes the target node, by removing the current one
            }
            actionPoints -= 1;
        }
        else
        {
            isShipMoving = false;
        }

        if (actionPoints == 0) { transform.position = previousPosition; }//If the ship is out of action points, the ship returns to the previous node

        if (isShipSelected) { gameObject.layer = 0; } else { gameObject.layer = 8; }//If the ship is selected, it is no longer counted as a wall
    }

    private void OnMouseDown()
    {
        if (!selector.isOverUI)
        {
            selectedMouse = true;
            GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].GetComponent<ShipControl>().isShipMoving == true) { return; }
                ships[i].GetComponent<ShipControl>().isShipSelected = false;//Changes the selected status of all ship to false
            }

            isShipSelected = true; //Changes the selected status of this ship to true
            selector.currentShip = gameObject;
            user.selectedShipName.text = shipName;
            user.actNum = (0);//Resets the UI to give the default settings
            user.ResetText();
            Debug.Log("Ship Clicked");
        }
    }

    private void OnMouseExit()
    {
        selectedMouse = false;
    }

    public void FireWeapon(Vector2 direction)
    {
        float accNum = 10 - accuracy;
        float accX = Random.Range(0-accNum, accNum);
        float accY = Random.Range(0 - accNum, accNum);//Forms an "innaccuracy" vector to affect shots

        Vector2 accVect = ((Vector2)transform.position + new Vector2(accX, accY)); //Accuracy is affected and changes the vector
        Vector2 dir = (direction - accVect).normalized;//Returned the direction of the shot, normalized to a magnitude of 1.
        GameObject newObject = Instantiate(shot, transform.position, Quaternion.identity);
        if (!newObject.GetComponent<ShotScript>()) { newObject.AddComponent<ShotScript>(); }//If the prefab doesn't have the correct script, add it

        newObject.GetComponent<ShotScript>().direction = dir;
        newObject.GetComponent<ShotScript>().start = transform.position;
        newObject.GetComponent<ShotScript>().speed = shotSpeed;
        newObject.GetComponent<ShotScript>().distance = shotRange;//Sets the properties of the shot, according to the ship firing
    }
}
