using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This script takes the data held about ships in the player profile and gets it ready to be used by the map generator. Also adds game objects to attach the data to.
public class ShipStorage : MonoBehaviour
{
    public Profile player;

    public GameObject ship;
    public GameObject enemy;
        
    public List<GameObject> storedShips;//The list of ships that the player can potentially control
    public List<GameObject> enemyShips;

	// Use this for initialization
	void Start ()
    {
        PopulateEnemyShips(PlayerProfile.enemyNumber);
	}
		
    public void PopulateEnemyShips(int numberOfEnemies)
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            enemyShips.Add(enemy);//Fills the list of enemies will the required amount
        }
    }

    public void ShipList()
    {
        player = PlayerProfile.player;//Loads the tester as the profile for the player
        List<Ship> currentShips = player.playerShips;// copies the list of player ships
        if (currentShips !=null)
        {
            for (int i = 0; i < currentShips.Count; i++)
            {
                storedShips.Add(ship);//Creates ships in the ship list for the player to control
                storedShips[i].GetComponent<ShipControl>().ship = currentShips[i];//Assigns the ship identity to the blank ships
            }
        }
    }
}
