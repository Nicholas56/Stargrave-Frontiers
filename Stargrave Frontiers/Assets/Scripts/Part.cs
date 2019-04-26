using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
