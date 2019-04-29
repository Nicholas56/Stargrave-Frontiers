using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Nicholas Easterby - EAS12337350
//This script handles the individual actions of each ship. This script is fed the path to follow to create movement
//This script also controls ships selection and ship firing
public class ShipControl : MonoBehaviour
{
    public bool isShipSelected;
    public bool isShipMoving;

    public int shipId;
    public Ship ship;
    public int actionPoints;
    public GameObject shot;
    public float shotSpeed;

    public bool canMove;
    public float speed;
    public float waypointProximity;    

    MoveSelectShip selector;
    UIController user;
    public List<Node> pathToTake;
    Vector2 previousPosition;

    public bool selectedMouse;

    GameObject exhaust;
    
    // Use this for initialization
    void Start ()
    {
        selector = FindObjectOfType<MoveSelectShip>();
        user = FindObjectOfType<UIController>();
        pathToTake = new List<Node>();
        canMove = true;

        actionPoints = 310;
        exhaust = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (pathToTake.Count != 0 && actionPoints > 0 && canMove)
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

        if (isShipMoving && exhaust) { exhaust.SetActive(true); } else { exhaust.SetActive(false); }//Shows the exhaust animation, when the ship is moving

        if (pathToTake.Count > 0)
        {
            Vector2 localVelocity = new Vector2(pathToTake[0].Position.x - transform.position.x, pathToTake[0].Position.y - transform.position.y);
            if (localVelocity.x > 0 && localVelocity.y == 0)
            { transform.eulerAngles = new Vector3(0, 0, -90); }
            else if (localVelocity.x < 0 && localVelocity.y == 0)
            { transform.eulerAngles = new Vector3(0, 0, 90); }
            else if (localVelocity.x == 0 && localVelocity.y > 0)
            { transform.eulerAngles = new Vector3(0, 0, 0); }
            else if (localVelocity.x == 0 && localVelocity.y < 0)
            { transform.eulerAngles = new Vector3(0, 0, 180); }
        }
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
            user.selectedShipName.text = ship.shipName;
            user.actNum = (0);//Resets the UI to give the default settings
            user.ResetText();
        }
    }

    private void OnMouseExit()
    {
        selectedMouse = false;
    }

    public void FireWeapon(Vector2 direction)
    {
        if (actionPoints > ship.weapon.actionCost)
        {
            actionPoints -= ship.weapon.actionCost;//subtracts the cost for firing the weapon

            float accNum = (10 - ship.weapon.accuracy) / 2;
            float accX = Random.Range(0 - accNum, accNum);
            float accY = Random.Range(0 - accNum, accNum);//Forms an "innaccuracy" vector to affect shots

            Vector2 accVect = ((Vector2)transform.position + new Vector2(accX, accY)); //Accuracy is affected and changes the vector
            Vector2 dir = (direction - accVect).normalized;//Returned the direction of the shot, normalized to a magnitude of 1.
            if (GetComponent<Collider2D>()) { GetComponent<Collider2D>().enabled = false; }//Turns the collider off, while firing
            GameObject newObject = Instantiate(shot, transform.position, Quaternion.identity);
            if (!newObject.GetComponent<ShotScript>()) { newObject.AddComponent<ShotScript>(); }//If the prefab doesn't have the correct script, add it

            newObject.GetComponent<ShotScript>().direction = dir;
            newObject.GetComponent<ShotScript>().start = transform.position;
            newObject.GetComponent<ShotScript>().speed = shotSpeed;
            newObject.GetComponent<ShotScript>().distance = ship.weapon.shotRange;//Sets the properties of the shot, according to the ship firing

            Invoke("EnableCollider", 0.5f);
        }
        else
        {
            user.FlashNotification();
        }
    }

    public void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;//Turns the collider back on
    }
}
