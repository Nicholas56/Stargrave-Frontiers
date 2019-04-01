using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    Profile player;

    public Vector2Int startArea;
    public int obstacleNumber;
    public ShipStorage ships;

    public GameObject obstacle;
    List<Vector2> obstaclePositions;
    Grid grid;
    int gX;
    int gY;

    // Use this for initialization
    void Start()
    {
        grid = GetComponent<Grid>();
        ships = FindObjectOfType<ShipStorage>();
        Invoke("PlaceObstacles", 1);
    }

    void PlaceObstacles()
    {
        gX = grid.grid.GetLength(0);//Gets number of nodes along the x axis
        gY = grid.grid.GetLength(1);// Gets number of nodes along the y axis
        obstaclePositions = new List<Vector2>();//Makes a new list of the obstacle positions

        for (int i = 0; i < obstacleNumber + ships.enemyShips.Count; i++)//Places as many obstacles as specified
        {
            int rand1 = Random.Range(0, gX);//Chooses a random x node, 
            int rand2 = Random.Range(0, gY);//Chooses a random y node, 
            int rand3 = Random.Range((gX - startArea.x), gX);//Chooses a random x node, near top right of map
            int rand4 = Random.Range((gY - startArea.y), gY);//Chooses a random y node, near top right of map

            Vector2 pos1 = grid.grid[rand1, rand2].Position;// Gets the position of the chosen(random) node
            Vector2 pos2 = grid.grid[rand3, rand4].Position;// Gets the position of the chosen(random) node
            if (!obstaclePositions.Contains(pos1))//Checks that there not already an obstacle at that node
            {
                obstaclePositions.Add(pos1);//Marks down the position where an obstacle exists
                if (i >= ships.enemyShips.Count)
                {
                    Instantiate(obstacle, pos1, Quaternion.identity);//Creates the obstacle at the node position
                }
                else
                {
                    GameObject newObject = Instantiate(ships.enemyShips[i], pos2, Quaternion.identity);//Loads the enemy ships onto the map
                    if (newObject.GetComponent<EnemyAI>())
                    {
                        newObject.GetComponent<EnemyAI>().shipId = i;//Sets the id of the enemy ship, in the order generated
                    }
                }
            }
            else { i--; }//If position is occupied, loops one extra time
        }
        grid.CreateGrid();//Reloads grid to change node wall properties
        PlaceShips();//places the ships
    }

    void PlaceShips()
    {
        player = FindObjectOfType<Tester>().player;
        ships.ShipList();//Runs the function that populates the store ships list
        for (int i = 0; i < ships.storedShips.Count; i++)
        { 
            int rand1 = Random.Range(0, startArea.x);//Chooses a random x node, inside of the start area
            int rand2 = Random.Range(0, startArea.y);//Chooses a random y node, inside of the start area

            Vector2 pos3 = grid.grid[rand1, rand2].Position;// Gets the position of the chosen(random) node

            if (!obstaclePositions.Contains(pos3))//Checks that there not already an obstacle at that node
            {
                obstaclePositions.Add(pos3);//Marks down the position where an obstacle exists
                GameObject newObject = Instantiate(ships.storedShips[i], pos3, Quaternion.identity);//Creates the ship at the node position  
                if (newObject.GetComponent<ShipControl>())
                {
                    newObject.GetComponent<ShipControl>().shipId = i;
                    newObject.GetComponent<ShipControl>().shipName = player.playerShips[i].shipName;
                    newObject.GetComponent<ShipControl>().actionPoints = player.playerShips[i].actionPoints;//Sets the ship id, name and action points for each ship
                }
                Debug.Log(pos3);
            }
            else { i--; }//If position is occupied, loops one extra time
        }
        
    }

    
}
