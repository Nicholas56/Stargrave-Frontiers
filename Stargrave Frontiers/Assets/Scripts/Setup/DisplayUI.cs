using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    Text currentText;
    public enum TextType { none, money, shopShip, shopWeapon, shopPart, playerShip, playerWeapon, playerPart, shipList, mapSize, enemyNumber, destroyedShips, bonus}
    public TextType type;//Designated the type of text that is being displayed
    public int item;

    int quantity;

    // Start is called before the first frame update
    void Start()
    {
        currentText = GetComponent<Text>();//Gets the attached text
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)//Depending on the type of text, how the scipt will display it
        {
            case TextType.money:
                currentText.text = "$: " + PlayerProfile.money;
                break;
            case TextType.destroyedShips:
                currentText.text = (PlayerProfile.score.currentEarnings/50) + " x $50 = $" + PlayerProfile.score.currentEarnings;
                break;
            case TextType.bonus:
                if (PlayerProfile.score.bonusNum == PlayerProfile.player.playerShips.Count && PlayerProfile.score.currentEarnings>200)
                {
                    currentText.text = "Achieved! $:200";//If conditions are met, bonus message is given
                }
                else {currentText.text = "No Bonus";}
                break;
            case TextType.mapSize:
                currentText.text = "Map Size: " + PlayerProfile.mapSize;
                break;
            case TextType.enemyNumber:
                currentText.text = "Opponents: " + PlayerProfile.enemyNumber;
                break;
            case TextType.shipList:
                currentText.text = "";
                for (int i = 0; i < PlayerProfile.player.playerShips.Count;i++) {
                    currentText.text =currentText.text + PlayerProfile.player.playerShips[i].shipName + " - " + PlayerProfile.player.playerShips[i].actionPoints.ToString() + " Action Points\n"; }
                break;
            case TextType.shopShip:
                quantity = 0;
                for (int i = 0; i < PlayerProfile.player.playerShips.Count; i++) { if (PlayerProfile.player.playerShips[i].sCost == PlayerProfile.gameShips[item].sCost) { quantity++; } }//Counts how many of this item the player owns
                currentText.text = "Item " + (item + 1) + ": " + PlayerProfile.gameShips[item].shipName + "    Cost: $" + PlayerProfile.gameShips[item].sCost + "   Qty: " + quantity;
                break;
            case TextType.shopWeapon:
                quantity = 0;
                for (int i = 0; i < PlayerProfile.player.weaponList.Count; i++) { if (PlayerProfile.player.weaponList[i].weaponName == PlayerProfile.gameWeapons[item].weaponName) { quantity++; } }//Counts how many of this item the player owns
                currentText.text = "Item " + (item + 1) + ": " + PlayerProfile.gameWeapons[item].weaponName + "    Cost: $" + PlayerProfile.gameWeapons[item].wCost + "   Qty: " + quantity;
                break;
            case TextType.shopPart:
                quantity = 0;
                for (int i = 0; i < PlayerProfile.player.partList.Count; i++) { if (PlayerProfile.player.partList[i].partName == PlayerProfile.gameParts[item].partName) { quantity++; } }//Counts how many of this item the player owns
                currentText.text = "Item " + (item + 1) + ": " + PlayerProfile.gameParts[item].partName + "    Cost: $" + PlayerProfile.gameParts[item].pCost + "   Qty: " + quantity;
                break;
            case TextType.playerPart:
                if (item < 0)
                {
                    item = 0;
                }
                if (item == PlayerProfile.player.partList.Count)
                {
                    item--;
                }
                if (PlayerProfile.player.partList.Count > 0)//Checks if the list exists and has more than one item
                {
                    currentText.text = PlayerProfile.player.partList[item].partName + " Sale: $" + PlayerProfile.player.partList[item].pCost / 2;
                }
                else { currentText.text = "No Parts"; }
                break;
            case TextType.playerShip:
                if (item < 0)
                {
                    item = 0;
                }
                if (item == PlayerProfile.player.playerShips.Count)
                {
                    item--;
                }
                if (PlayerProfile.player.playerShips.Count > 0)
                {
                    currentText.text = PlayerProfile.player.playerShips[item].shipName + " Sale: $" + PlayerProfile.player.playerShips[item].sCost / 2;
                }
                else { currentText.text = "No Ships"; }
                break;
            case TextType.playerWeapon:
                if (item < 0)
                {
                    item = 0;
                }
                if (item == PlayerProfile.player.weaponList.Count)
                {
                    item--;
                }
                if (PlayerProfile.player.weaponList.Count > 0)
                {
                    currentText.text = PlayerProfile.player.weaponList[item].weaponName + " Sale: $" + PlayerProfile.player.weaponList[item].wCost / 2;
                }
                else { currentText.text = "No Weapons"; }
                break;
            case TextType.none:
                break;
            default:
                break;
        }
    }
}
