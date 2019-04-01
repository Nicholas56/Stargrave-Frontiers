using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool isShipSelected;
    public bool isShipMoving;

    public int shipId;
    public int actionPoints;
    public string shipName;

    public float speed = 2;
    public float waypointProximity = 0.001f;
    public float searchRadius = 6;

    public List<Node> pathToTake;
    Vector2 previousPosition;

    // Use this for initialization
    void Start()
    {
        pathToTake = new List<Node>();

        actionPoints = 310;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShipSelected)
        {
            if (0 == actionPoints % 31)//Every 31 action points, the enemy uses the seek player function
            {
                SeekPlayer();
            }

            if (pathToTake.Count != 0 && actionPoints > 0)
            {
                isShipMoving = true;
                float step = speed * Time.deltaTime;//Sets the speed that the ship moves at

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

            if (isShipSelected) { gameObject.layer = 0; } 
        }
        else { gameObject.layer = 8; }//If the ship is selected, it is no longer counted as a wall

        if (actionPoints == 0) { transform.position = previousPosition; }//If the ship is out of action points, the ship returns to the previous node
    }

    public void SeekPlayer()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, searchRadius);//Searches for all colliders in a radius around this transform
        foreach(Collider2D ship in objects)
        {
            if(ship.tag == "Ship" && actionPoints>31)//If the collider belongs to a ship, and this enemy has enough action points
            {
                actionPoints -= 31;
                FireWeapon(ship.transform.position);
            }
        }        
    }

    void FireWeapon(Vector2 target)
    {

    }
}
