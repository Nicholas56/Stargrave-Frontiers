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

    //public Vector2 shipPosition;


    public Ship()
    {

    }

    public Ship(string name, int points, Weapon shipWeapon)
    {
        shipName = name;
        actionPoints = points;
        weapon = shipWeapon;
    }
}
