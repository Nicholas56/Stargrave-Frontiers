using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
