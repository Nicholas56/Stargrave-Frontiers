using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public string shipName;

    public int actionPoints;

    public Weapon weapon;

    public GameObject shot;    
    public float shotSpeed;

    public List<Part> parts;

    public Ship()
    {

    }

    public Ship(string name, int points, Weapon shipWeapon)
    {
        shipName = name;
        actionPoints = points;
        weapon = shipWeapon;
    }

    public Ship(string name, int points, Weapon shipWeapon, List<Part> shipParts)
    {
        shipName = name;
        actionPoints = points;
        weapon = shipWeapon;
        parts = shipParts;
    }
}
