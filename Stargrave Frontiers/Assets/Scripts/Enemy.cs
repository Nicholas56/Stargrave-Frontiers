using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string typeName;

    public int actionPoints;

    public Enemy(string name, int points)
    {
        typeName = name;
        actionPoints = points;
    }
}
