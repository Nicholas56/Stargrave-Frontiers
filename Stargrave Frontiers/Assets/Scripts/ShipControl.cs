using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public bool isShipSelected;
    public int actionPoints;

    public float speed;
    public float waypointProximity;

    MoveSelectShip selector;
    public List<Node> pathToTake;

    // Use this for initialization
    void Start ()
    {
        selector = FindObjectOfType<MoveSelectShip>();
        pathToTake = new List<Node>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pathToTake.Count != 0)
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, pathToTake[0].Position, step);//The ship moves towards the location of the tile in the path
            if (Vector2.Distance(transform.position, pathToTake[0].Position) < waypointProximity)//When the ship is close enough to the tile, the waypoint is changed
            {
                pathToTake.RemoveAt(0);
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Ship Clicked");
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < ships.Length; i++) { ships[i].GetComponent<ShipControl>().isShipSelected = false; } //Changes the selected status of all ship to false

        isShipSelected = true; //Changes the selected status of this ship to true
        selector.currentShip = gameObject;
    }
}
