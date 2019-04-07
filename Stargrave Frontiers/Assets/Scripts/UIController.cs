using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text selectedShipName;
    public Text outOfActionPoints;

    public Button actionButton;
    public Button endTurnButton;
    List<string> actions;
    public int actNum;

    public Image actionBar;

    MoveSelectShip selector;

	// Use this for initialization
	void Start ()
    {
        selector = FindObjectOfType<MoveSelectShip>();
        actions = new List<string>();
        actions.Add("Move");
        actions.Add("Fire");

        outOfActionPoints.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selector.currentShip)
        {
            float actPoints = (float)selector.currentShip.GetComponent<ShipControl>().actionPoints;//Converts int to float for calculation
            actionBar.fillAmount =  actPoints/ 1000;//Shows the action bar filled as a ratio of remaining action points
        }
        else
        {
            actionBar.fillAmount = 0;
            selectedShipName.text = "";
            actNum = (0);//The number resets
            actionButton.GetComponentInChildren<Text>().text = actions[actNum];//Changes the button name
        }

        if (selector.isTurn == false) { endTurnButton.interactable = false; } else { endTurnButton.interactable = true; }//If it is your turn the button is interactible
	}

    public void ChangeAction()
    {
        if (selector.currentShip && !selector.currentShip.GetComponent<ShipControl>().isShipMoving)
        {
            actNum = (actNum + 1) % actions.Count;//The number cycles through possible configurations
            actionButton.GetComponentInChildren<Text>().text = actions[actNum];//Changes the button name
            if (actNum == 0) { selector.currentShip.GetComponent<ShipControl>().canMove = true; }//If the current action is "Move", sets the current ship status to 'can move'
            else { selector.currentShip.GetComponent<ShipControl>().canMove = false; }
        }
    }

    public void EndTurn()
    {
        selector.isTurn = false;
        selector.currentShip = null;
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].GetComponent<EnemyAI>().actionPoints = 310;//resets the enemy action points to full
        }
    }

    public void ResetText()
    {
        actionButton.GetComponentInChildren<Text>().text = actions[actNum];//Changes the button name
    }

    public void FlashNotification()
    {
        outOfActionPoints.gameObject.SetActive(true);//Make the notification visible
        Invoke("RemoveNotification", 0.5f);
    }

    public void RemoveNotification()
    {
        outOfActionPoints.gameObject.SetActive(false);//Make the notification invisible
    }
}
