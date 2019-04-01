using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text selectedShipName;

    public Button actionButton;
    List<string> actions;
    public int actNum;

    public Image actionBar;

    MoveSelectShip selector;
    EnemyAIController enemy;

	// Use this for initialization
	void Start ()
    {
        selector = FindObjectOfType<MoveSelectShip>();
        enemy = FindObjectOfType<EnemyAIController>();
        actions = new List<string>();
        actions.Add("Move");
        actions.Add("Fire");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selector.currentShip)
        {
            float actPoints = (float)selector.currentShip.GetComponent<ShipControl>().actionPoints;//Converts int to float for calculation
            actionBar.fillAmount =  actPoints/ 1000;//Shows the action bar filled as a ratio of remaining action points
        }
	}

    public void ChangeAction()
    {
        if (selector.currentShip)
        {
            actNum = (actNum + 1) % actions.Count;//The number cycles through possible configurations
            actionButton.GetComponentInChildren<Text>().text = actions[actNum];//Changes the button name
            if (actNum == 0) { selector.currentShip.GetComponent<ShipControl>().canMove = true; }
            else { selector.currentShip.GetComponent<ShipControl>().canMove = false; }
        }
    }

    public void EndTurn()
    {
        selector.isTurn = false;
        selector.currentShip = null;
        enemy.EnemyTurn();//Deselects the player ships and begins the enemy turn
    }

}
