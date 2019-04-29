using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This defines what an enemy is and what attributes it has. Also contains the constructor for an enemy
public class Enemy
{
    public string typeName;

    public int actionPoints;
    public int hitPoints;
    public float accuracy;
    public float range;

    public Enemy(string name, int points, float acc, float shotRange, int hits)
    {
        typeName = name;
        actionPoints = points;
        accuracy = acc;
        range = shotRange;
        hitPoints = hits;
    }
}
