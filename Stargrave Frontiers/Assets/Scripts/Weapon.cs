﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string weaponName;

    public int actionCost;
    public float shotRange;
    public float accuracy;//subtracted from 10
      
    public Weapon(string name, int aCost, float range,float acc)
    {
        weaponName = name;
        actionCost = aCost;
        shotRange = range;
        accuracy = acc;
    }
}
