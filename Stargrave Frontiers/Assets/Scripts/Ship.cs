using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//Defines a ship and its properties. Features constructors for ships with and without attached parts.
public class Ship 
{
    public string shipName;

    public int actionPoints;

    public Weapon weapon;
  
    public List<Part> parts;

    public int partLimit;

    public int sCost;

    public Ship()
    {

    }

    public Ship(string name, int points, Weapon shipWeapon, int cost, int limit)
    {
        shipName = name;
        actionPoints = points;
        weapon = shipWeapon;
        sCost = cost;
        partLimit = limit;
        parts = new List<Part>(4);
    }

    public Ship(string name, int points, Weapon shipWeapon, int cost, int limit, List<Part> shipParts)
    {
        shipName = name;
        actionPoints = points;
        weapon = shipWeapon;
        sCost = cost;
        partLimit = limit;
        parts = shipParts;
    }
}
