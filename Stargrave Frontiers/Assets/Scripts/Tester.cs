using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public Profile player;

	// Use this for initialization
	void Start ()
    {
        player = new Profile();
        player.weaponList.Add(new Weapon("Pulse Cannon", 31, 10f, 6f, 50));
        player.partList.Add(new Part("Hull", 30));

        player.playerShips.Add(new Ship("Excelsior", 310, player.weaponList[0], 200,4,GenerateParts()));
        player.playerShips.Add(new Ship("Destroyer", 310, player.weaponList[0], 200,4,GenerateParts()));
        player.playerShips.Add(new Ship("Gladius", 310, player.weaponList[0], 200,4,GenerateParts()));
        player.playerShips.Add(new Ship("Monster", 310, player.weaponList[0], 200,4,GenerateParts()));
        player.playerShips.Add(new Ship("Enterprise", 310, player.weaponList[0],200,4, GenerateParts()));
        player.playerShips.Add(new Ship("Apollo", 310, player.weaponList[0],200, 4,GenerateParts()));
        player.enemyTypes.Add(new Enemy("Frigate", 310, 6.5f, 10f, 1));                
	}	

    List<Part> GenerateParts()
    {
        List<Part> partsList = new List<Part>();
        partsList.Add(new Part("Hull", 30));

        return partsList;
    }
}
