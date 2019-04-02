using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public string shipName;

    public int actionPoints;

    public GameObject shot;
    public float accuracy;//subtracted from 10
    public float shotSpeed;
    public float shotRange;
    //public Vector2 shipPosition;


    public Ship()
    {

    }

    public Ship(string name, int points)
    {
        shipName = name;
        actionPoints = points;
    }
}
