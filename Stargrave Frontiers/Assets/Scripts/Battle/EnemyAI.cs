using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This is the controller for an individual enemy vessel. This calculates the movement path, using the pathfinder script.
//This also calculates where to fire, upon locating a player ship.
//This script holds the enemy shot and the enemy exhaust gameobjects.
//This script also handles the ships action points and their use

public class EnemyAI : MonoBehaviour
{
    public bool isShipSelected;
    public bool isShipMoving;

    public int shipId;
    public int actionPoints;
    public string shipName;
    public GameObject enemyShot;
    public float shotSpeed = 5;
    public float shotAcc;
    public float shotRange;
    public int hitPoints;
    float shotTimer;
    bool canShoot;

    GameObject exhaust;

    public float speed = 2;
    public float waypointProximity = 0.001f;
    public float searchRadius = 6;

    public List<Node> pathToTake;
    Vector2 previousPosition;

    // Use this for initialization
    void Start()
    {
        pathToTake = new List<Node>();
        canShoot = true;
        actionPoints = 310;
        exhaust = transform.GetChild(0).gameObject;
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

        if (isShipMoving && exhaust) { exhaust.SetActive(true); } else { exhaust.SetActive(false); }//Shows the exhaust animation, when the ship is moving
    }

    public void SeekPlayer()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, searchRadius);//Searches for all colliders in a radius around this transform
        foreach(Collider2D ship in objects)
        {
            if(Time.time >= shotTimer + 1.5f) { canShoot = true; }
            if(ship.tag == "Ship" && actionPoints>31 && canShoot)//If the collider belongs to a ship, and this enemy has enough action points
            {
                canShoot = false;
                actionPoints -= 31;
                FireWeapon(ship.transform.position);//Takes off the action points, and fires at location of player
                shotTimer = Time.time;
            }
        }        
    }

    void FireWeapon(Vector2 target)
    {
        float accNum = (10 - shotAcc) / 2;
        float accX = Random.Range(0 - accNum, accNum);
        float accY = Random.Range(0 - accNum, accNum);//Forms an "innaccuracy" vector to affect shots

        Vector2 accVect = ((Vector2)transform.position + new Vector2(accX, accY)); //Accuracy is affected and changes the vector
        Vector2 dir = (target - accVect).normalized;//Returned the direction of the shot, normalized to a magnitude of 1.

        GetComponent<Collider2D>().enabled = false;//Turns the collider off, while firing
        GameObject newObject = Instantiate(enemyShot, transform.position, Quaternion.identity);
        if (!newObject.GetComponent<ShotScript>()) { newObject.AddComponent<ShotScript>(); }//If the prefab doesn't have the correct script, add it

        newObject.GetComponent<ShotScript>().direction = dir;
        newObject.GetComponent<ShotScript>().start = transform.position;
        newObject.GetComponent<ShotScript>().speed = shotSpeed;
        newObject.GetComponent<ShotScript>().distance = shotRange;//Sets the properties of the shot, according to the ship firing

        Invoke("EnableCollider", 2f);
    }

    public void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;//Turns the collider back on
    }
}
