using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    //GameObject currentEnemy;
    Grid grid;
    Pathfinding paths;
    public GameObject targetPoint;
    bool moveLeft;

    MoveSelectShip gameTurn;

	// Use this for initialization
	void Start ()
    {
        gameTurn = GetComponent<MoveSelectShip>();
        //currentEnemy = null;
        grid = GetComponent<Grid>();
        paths = GetComponent<Pathfinding>();
        moveLeft = true;
    }
	
    public void EnemyTurn()
    {
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].GetComponent<ShipControl>().isShipSelected = false;//Changes the selected status of all ship to false
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//Finds all the enemy vessels
        for (int i = 0; i < enemies.Length; i++)
        {
            grid.CreateGrid();//Sets the walls in the grid
            paths.StartPosition = enemies[i].transform;//Sets the start point of the path as the current ship in the list
            if (enemies[i].transform.position.x > grid.grid[1, 0].Position.x && moveLeft)
            {
                int randY = Random.Range(0, grid.grid.GetLength(1));//Chooses a random y node,
                GameObject newObject = Instantiate(targetPoint, grid.grid[0, randY].Position, Quaternion.identity);//Creates a target at a random node on the left of the map
                paths.TargetPosition = newObject.transform;
                Destroy(newObject);//Destroys the created target
                int count = 0;
            label:
                if (grid.FinalPath != null)
                {
                    if (enemies[i].GetComponent<EnemyAI>())
                    {
                        enemies[i].GetComponent<EnemyAI>().pathToTake = grid.FinalPath;//Sets the path of the enemy
                        grid.FinalPath = null;//Resets the final path in Grid
                    }
                }
                else
                {
                    count++;
                    if (count > 15) { goto label2; }//Ensures that the path is set,by looping until the path is not null
                    goto label;
                }
            label2:;
            }
        }
        gameTurn.isTurn = true;
    }
}
