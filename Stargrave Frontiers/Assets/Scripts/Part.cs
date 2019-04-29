using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//Defines a part and its properties. Contains the constructor
public class Part {

    public string partName;
    public int pCost;
    public bool inUse;

    public Part(string name, int cost)
    {
        partName = name;
        pCost = cost;
    }

}
