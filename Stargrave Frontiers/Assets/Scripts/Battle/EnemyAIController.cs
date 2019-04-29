using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    GameObject currentEnemy;
    Grid grid;
    Pathfinding paths;
    public GameObject targetPoint;

    GameObject[] enemies;
    bool moveLeft;
    int enemyTurn;

    MoveSelectShip gameTurn;

	// Use this for initialization
	void Start ()
    {
        gameTurn = GetComponent<MoveSelectShip>();
        currentEnemy = null;
        grid = GetComponent<Grid>();
        paths = GetComponent<Pathfinding>();
        moveLeft = true;
        enemyTurn = 0;
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Enemy") && gameTurn.isTurn == false && enemyTurn<50)
        {
            grid.CreateGrid();//Sets the walls in the grid
            if (enemyTurn == 0)
            {
                GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
                for (int i = 0; i < ships.Length; i++)
                {
                    ships[i].GetComponent<ShipControl>().isShipSelected = false;//Changes the selected status of all ship to false
                }
            }
            enemies = GameObject.FindGameObjectsWithTag("Enemy");//Finds all the enemy vessels
            EnemyTurns(enemyTurn);
        }
    }
    
    public void EnemyTurns(int enemy)
    {
        Debug.Log(enemy);
        if (enemy < enemies.Length)
        {
            GameObject ship = enemies[enemy];

            enemyTurn = (enemyTurn + 1) * 100;

            paths.StartPosition = ship.transform;//Sets the start point of the path as the current ship in the list
            if (moveLeft && !GameObject.FindGameObjectWithTag("Target"))
            {
                int randY = Random.Range(0, grid.grid.GetLength(1));//Chooses a random y node,
                GameObject newObject = Instantiate(targetPoint, grid.grid[0, randY].Position, Quaternion.identity);//Creates a target at a random node on the left of the map
                paths.TargetPosition = newObject.transform;
                Destroy(newObject, 1);//Destroys the created target
            }
            else if(!GameObject.FindGameObjectWithTag("Target"))
            {
                int randY = Random.Range(0, grid.grid.GetLength(1));//Chooses a random y node,
                GameObject newObject = Instantiate(targetPoint, grid.grid[grid.grid.GetLength(0)-1, randY].Position, Quaternion.identity);//Creates a target at a random node on the left of the map
                paths.TargetPosition = newObject.transform;
                Destroy(newObject, 1);//Destroys the created target
            }
            currentEnemy = ship;
        }
        Invoke("LoadPath", 2);//Waits half a second to load the path
    }

    void LoadPath()
    {
        if (grid.FinalPath != null)//Checks that there is a path
        {
            if (currentEnemy && currentEnemy.GetComponent<EnemyAI>())
            {
                currentEnemy.GetComponent<EnemyAI>().isShipSelected = true;
                currentEnemy.GetComponent<EnemyAI>().pathToTake = grid.FinalPath;//Sets the path of the enemy
                grid.FinalPath = null;//Resets the final path in Grid
            }
        }
        enemyTurn = enemyTurn / 100;
        if (currentEnemy.transform.position.x > grid.grid[grid.grid.GetLength(0)-2, 0].Position.x){ moveLeft = true; }//If the ship is close to the right edge, direction changes
        if (currentEnemy.transform.position.x < grid.grid[1, 0].Position.x){ moveLeft = false; }//If the ship is close to the left edge, direction changes
        Debug.Log(moveLeft);
        if (enemyTurn == enemies.Length)
        {
            Invoke("EndEnemyTurn", 1);
        }
    }

    void EndEnemyTurn()
    {
        gameTurn.isTurn = true;//It becomes the players turn again
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].GetComponent<ShipControl>().actionPoints = 310;//Sets all the player ships to full action points
            //ships[i].GetComponent<ShipControl>().pathToTake.RemoveRange(1, ships[i].GetComponent<ShipControl>().pathToTake.Count-1);//resets ship paths  ISSUE need to reset path each turn
        }
        foreach (GameObject ship in enemies)
        {
            if (ship.GetComponent<EnemyAI>())
            {
                ship.GetComponent<EnemyAI>().isShipSelected = false;//Changes the selected status of all ship to false
            }
        }
        enemyTurn = 0;//Resets the enemy turn counter
    }
}
