using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStorage : MonoBehaviour
{
    public GameObject ship;
    public GameObject enemy;
        
    public List<GameObject> storedShips;//The list of ships that the player can potentially control
    public List<GameObject> enemyShips;

	// Use this for initialization
	void Start ()
    {
        PopulateEnemyShips(6);
	}
		
    public void PopulateEnemyShips(int numberOfEnemies)
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            enemyShips.Add(enemy);//Fills the list of enemies will the required amount
        }
    }

    public void ShipList(GameObject spaceShip)
    {
        storedShips.Add(spaceShip);//Adds the described spaceship to the list
    }
}
