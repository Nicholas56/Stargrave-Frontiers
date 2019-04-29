using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This defines the properties of a node as well as providing the constructor for new nodes
//This script was created using a turorial by Daniel on youtube - link:https://www.youtube.com/watch?v=AKKpPmxx07w  
public class Node
{
    public int gridX; // X position in the Node Array
    public int gridY; // Y position in the Node Array

    public bool IsWall; //Tells the program if this node is being obstructed.
    public Vector2 Position; //The world position of the node

    public Node Parent; //For the AStar algorithm, will store what node it previously came from so it can trace the shortest path

    public int gCost; //The cost of moving to the next square
    public int hCost; //The distance to the goal from this node

    public int Fcost { get { return gCost + hCost; } } //Quick get function to add G cost and H cost, and since F cost is never edited, no set function

    public Node(bool a_IsWall, Vector2 a_Pos, int a_gridX, int a_gridY) //Constructor
    {
        IsWall = a_IsWall; //Tells the program if this node is being obstructed
        Position = a_Pos; //The world position of the node
        gridX = a_gridX; //X Position in the Node Array
        gridY = a_gridY; //Y Position in the Node Array
    }
    
}
