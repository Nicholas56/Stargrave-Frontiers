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
    public Ship player;

	// Use this for initialization
	void Start ()
    {
        selector = FindObjectOfType<MoveSelectShip>();
        actions = new List<string>();
        actions.Add("Move");
        actions.Add("Fire");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selector.currentShip)
        {
            actionBar.fillAmount = selector.currentShip.GetComponent<ShipControl>().actionPoints / 1000;
            selectedShipName.text = player.shipName;
        }
	}

    public void ChangeAction()
    {
        actNum = (actNum + 1) % actions.Count;//The number cycles through possible configurations
        actionButton.GetComponentInChildren<Text>().text = actions[actNum];//Changes the button name
        if (actNum == 0) { selector.currentShip.GetComponent<ShipControl>().canMove = true; }
        else { selector.currentShip.GetComponent<ShipControl>().canMove = false; }
    }

}
