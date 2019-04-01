using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile
{

    public List<Ship> playerShips;

    public List<Enemy> enemyTypes;

	public Profile()
    {
        playerShips = new List<Ship>();
        enemyTypes = new List<Enemy>();
    }
}
