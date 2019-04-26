using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public enum TextType { none, BuyShip, BuyWeapon, BuyPart, SellShip, SellWeapon, SellPart, previous, next, random, endBattle }
    public TextType type;//Designated the type of button that is being pressed
    public int item;

    // Start is called before the first frame update
    void Update()
    {
        switch (type)//Depending on the type of button, how the scipt will perform
        {
            case TextType.SellShip:
                item = GetComponent<DisplayUI>().item;
                break;
            case TextType.SellWeapon:
                item = GetComponent<DisplayUI>().item;
                break;
            case TextType.SellPart:
                item = GetComponent<DisplayUI>().item;
                break;
            default:break;
        }
    }

    // Update is called once per frame
    public void PressButton()
    {
        switch (type)//Depending on the type of button, how the scipt will perform
        {
            case TextType.BuyShip:
                PlayerProfile.PurchaseShip(item);
                break;
            case TextType.BuyWeapon:
                PlayerProfile.PurchaseWeapon(item);
                break;
            case TextType.BuyPart:
                PlayerProfile.PurchasePart(item);
                break;
            case TextType.endBattle:
                if (PlayerProfile.score.bonusNum == PlayerProfile.player.playerShips.Count && PlayerProfile.score.currentEarnings > 200)
                {
                    PlayerProfile.money += 200;//If conditions for bonus are met, add the correct amount.
                }
                MenuScript.EndBattle();
                break;
            case TextType.none:
                break;
            case TextType.random:
                PlayerProfile.RandomBattle();
                break;
            case TextType.SellShip:
                //item = GetComponent<DisplayUI>().item;
                PlayerProfile.SellShip(item);
                break;
            case TextType.SellWeapon:
                //item = GetComponent<DisplayUI>().item;
                PlayerProfile.SellWeapon(item);
                break;
            case TextType.SellPart:
                //item = GetComponent<DisplayUI>().item;
                PlayerProfile.SellPart(item);
                break;
            case TextType.previous:
                GetComponentInParent<DisplayUI>().item--;
                break;
            case TextType.next:
                GetComponentInParent<DisplayUI>().item++;
                break;
            default:
                break;
        }
    }
}
