using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This is the cursor script that attaches the game cursor to the grid. This works by getting the position of the pc cursor
public class Cursor : MonoBehaviour
{
    Grid grid;
    public GameObject cursor;
    float diameter;

	// Use this for initialization
	void Start ()
    {
        grid = FindObjectOfType<Grid>();
        diameter = grid.nodeRadius * 2;
        //Instantiate(cursor, grid.grid[0, 0].Position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int num1 = Mathf.RoundToInt(pos.x); int num2 = Mathf.RoundToInt(pos.y);
        //Vector2 pos2 = grid.grid[num1, num2].Position;
        //cursor.transform.position = ;
        if (cursor.transform.position.x > num1)
        {
            cursor.transform.position = new Vector2(cursor.transform.position.x-diameter, cursor.transform.position.y);
        }
        if (cursor.transform.position.x < num1)
        {
            cursor.transform.position = new Vector2(cursor.transform.position.x+diameter, cursor.transform.position.y);
        }
        if (cursor.transform.position.y > num2)
        {
            cursor.transform.position = new Vector2(cursor.transform.position.x, cursor.transform.position.y-diameter);
        }
        if (cursor.transform.position.y < num2)
        {
            cursor.transform.position = new Vector2(cursor.transform.position.x, cursor.transform.position.y+diameter);
        }
    }
}
