using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Transform StartPosition;//This is where the program will start the pathfinding from.
    public LayerMask WallMask;//This is the mask that the program will look for when trying to find obstructions to the path.
    public Vector2 gridWorldSize;//A vector2 to store the width and height of the graph in world units.
    public float nodeRadius;//This stores how big each square on the graph will be
    public float Distance;//The distance that the squares will spawn from eachother.

    public Node[,] grid;//The array of nodes that the A Star algorithm uses.
    public List<Node> FinalPath;//The completed path that the red line will be drawn along

    float nodeDiameter;//Twice the amount of the radius (Set in the start function)
    int gridSizeX, gridSizeY;//Size of the Grid in Array units.

    private void Start()
    {
        gridWorldSize = PlayerProfile.mapSize;
        nodeDiameter = nodeRadius * 2;//Double the radius to get diameter
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        CreateGrid();//Draw the grid
    }

    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];//Declare the array of nodes.
        Vector2 bottomLeft = (Vector2)transform.position - (Vector2.right * gridWorldSize.x / 2) - (Vector2.up * gridWorldSize.y / 2);//Get the real world position of the bottom left of the grid.
        for (int x = 0; x < gridSizeX; x++)//Loop through the array of nodes.
        {
            for (int y = 0; y < gridSizeY; y++)//Loop through the array of nodes.
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);//Get the world co ordinates of the bottom left of the graph
                bool Wall = true;//Make the node a wall

                //If the node is not being obstructed
                //Quick collision check against the current node and anything in the world at its position. If it is colliding with an object with a WallMask,
                //The if statement will return false.
                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, WallMask))
                {
                    Wall = false;//Object is not a wall
                }

                grid[x,y] = new Node(Wall, worldPoint, x,y);//Create a new node in the array.
            }
        }
    }

    //Gets the closest node to the given world position.
    public Node NodeFromWorldPosition(Vector2 a_WorldPosition)
    {
        float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_WorldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }

    //Function that gets the neighboring nodes of the given node.
    public List<Node> GetNeighborNodes(Node a_Node)
    {
        List<Node> NeighborNodes = new List<Node>();//Make a new list of all available neighbors.
        int xCheck;//Variable to check if the XPosition is within range of the node array to avoid out of range errors.
        int yCheck;//Variable to check if the YPosition is within range of the node array to avoid out of range errors.

        //Rightside
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if(xCheck >= 0 && xCheck < gridSizeX)//If the XPosition is in range of the array
        {
            if (yCheck >=0 && yCheck < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborNodes.Add(grid[xCheck, yCheck]);//Add the grid to the available neighbors list
            }
        }

        //Leftside
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)//If the XPosition is in range of the array
        {
            if (yCheck >= 0 && yCheck < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborNodes.Add(grid[xCheck, yCheck]);//Add the grid to the available neighbors list
            }
        }

        //Topside
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)//If the XPosition is in range of the array
        {
            if (yCheck >= 0 && yCheck < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborNodes.Add(grid[xCheck, yCheck]);//Add the grid to the available neighbors list
            }
        }

        //Bottomside
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)//If the XPosition is in range of the array
        {
            if (yCheck >= 0 && yCheck < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborNodes.Add(grid[xCheck, yCheck]);//Add the grid to the available neighbors list
            }
        }

        return NeighborNodes;//Return the neighbors list.
    }

    //Function that draws the wireframe
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));//Draw a wire cube with the dimensions given from the grid

        if (grid != null)// If the grid is not empty
        {
            foreach(Node node in grid)//Loop through every node in the grid
            {
                if (node.IsWall)//If the current node is a wall node
                {
                    Gizmos.color = Color.white;// Set the color of the node
                }
                else
                {
                    Gizmos.color = Color.yellow;// Set the color of the node
                }

                if (FinalPath != null)//If the final path is not empty
                {
                    Gizmos.color = Color.red;// Set the color of the node
                }

                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - Distance));//Draw the node at the position of the node
            }
        }
    }*/
}
