using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipControl : MonoBehaviour
{
    public bool isShipSelected;
    public bool isShipMoving;

    public int shipId;
    public int actionPoints;
    public string shipName;

    public bool canMove;
    public float speed;
    public float waypointProximity;

    MoveSelectShip selector;
    UIController user;
    public List<Node> pathToTake;
    Vector2 previousPosition;
    
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
        
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < ships.Length; i++)
        {
            if(ships[i].GetComponent<ShipControl>().isShipMoving == true) { return; }
            ships[i].GetComponent<ShipControl>().isShipSelected = false;//Changes the selected status of all ship to false
        } 

        isShipSelected = true; //Changes the selected status of this ship to true
        selector.currentShip = gameObject;
        user.selectedShipName.text = shipName;
        Debug.Log("Ship Clicked");
    }
}
