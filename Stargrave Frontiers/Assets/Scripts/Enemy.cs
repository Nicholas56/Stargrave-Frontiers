using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string typeName;

    public int actionPoints;
    public float accuracy;
    public float range;

    public Enemy(string name, int points, float acc, float shotRange)
    {
        typeName = name;
        actionPoints = points;
        accuracy = acc;
        range = shotRange;
    }
}
