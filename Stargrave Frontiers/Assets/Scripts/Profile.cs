using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//Defines the profile and its properties. Holds lists of game objects that the player has access to.
//Constructor creates new lists of each item
public class Profile
{
    public List<Weapon> weaponList;
    public List<Part> partList;


    public List<Ship> playerShips;

    public List<Enemy> enemyTypes;

	public Profile()
    {
        playerShips = new List<Ship>();
        weaponList = new List<Weapon>();
        partList = new List<Part>();

        enemyTypes = new List<Enemy>();
    }
}
