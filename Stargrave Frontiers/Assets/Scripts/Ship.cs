using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public string shipName;

    public int actionPoints;

    public Vector2 shipPosition;




    public Ship(string name, int points)
    {
        shipName = name;
        actionPoints = points;
    }
}
