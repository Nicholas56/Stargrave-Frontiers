using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSetupScript : MonoBehaviour
{
    public Text shipNumberTxt;
    public InputField shipName;
    public Text acPointsTxt;
    public Text shipWeaponTxt;
    public Text[] shipPartsTxt;

    private int index;

    public Text weaponListTxt;
    public Text partListTxt;

    private int weapIndex, partIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerProfile.player.playerShips.Count > 0)
        {
            shipName.text = PlayerProfile.player.playerShips[index].shipName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerProfile.player.playerShips.Count > 0)
        {
            shipNumberTxt.text = "Ship " + (index + 1) + ": \n" + PlayerProfile.player.playerShips[index].shipName;//Displays ship number and name

            PlayerProfile.player.playerShips[index].shipName = shipName.text;//Overwrite ship name with input field

            acPointsTxt.text = PlayerProfile.player.playerShips[index].actionPoints.ToString();//Displays the ships action points
            shipWeaponTxt.text = PlayerProfile.player.playerShips[index].weapon.weaponName +
                " - Range: " + PlayerProfile.player.playerShips[index].weapon.shotRange.ToString() +
                " - Acc: " + (PlayerProfile.player.playerShips[index].weapon.accuracy * 10).ToString() + "%";//Displays the weapons name, range and accuracy

            for(int i=0;i< PlayerProfile.player.playerShips[index].parts.Count && PlayerProfile.player.playerShips[index].parts.Count<= PlayerProfile.player.playerShips[index].partLimit; i++)//Modifes the text of parts for ship within the limit
            {
                shipPartsTxt[i].GetComponentInParent<Text>().text = "Part:";
                if (PlayerProfile.player.playerShips[index].parts[i] != null)//Checks to see if the ship has a part
                {
                    shipPartsTxt[i].text = PlayerProfile.player.playerShips[index].parts[i].partName;//Displays the name of the part
                }
                else { shipPartsTxt[i].text = "No part"; }//If there is no part, indicates as such
            }
            for(int i= PlayerProfile.player.playerShips[index].parts.Count; i < shipPartsTxt.Length; i++)
            {
                shipPartsTxt[i].GetComponentInParent<Text>().text = "";//For part text that isn't used, the text is set to blank
            }
        }
        else { shipNumberTxt.text = "No Ship"; shipName.GetComponentInChildren<Text>().text = ""; acPointsTxt.text = "";shipWeaponTxt.text = "";
            for (int i=0; i < shipPartsTxt.Length; i++) { shipPartsTxt[i].GetComponentInParent<Text>().text = ""; shipPartsTxt[i].text = ""; }//Sets everything to blank if there are no ships
        }

        if (PlayerProfile.player.weaponList.Count > 0)
        {
            weaponListTxt.text = PlayerProfile.player.weaponList[weapIndex].weaponName +
                " - Range: " + PlayerProfile.player.weaponList[weapIndex].shotRange + 
                " - Acc: " + (PlayerProfile.player.weaponList[weapIndex].accuracy * 10).ToString() + "%";//Displays the players weapons at the index, with range, and accuracy expressed as percentage
        }
        else { weaponListTxt.text = "No Weapons"; }

        if (PlayerProfile.player.partList.Count > 0)
        {
            partListTxt.text = PlayerProfile.player.partList[partIndex].partName;//Displays the players parts at the index
        }
        else { partListTxt.text = "No Parts"; }
    }

    public void PreviousShip()
    {
        if (index > 0)
        {
            index--;
            shipName.text = PlayerProfile.player.playerShips[index].shipName;
        }
    }

    public void NextShip()
    {
        if(index<PlayerProfile.player.playerShips.Count-1 && PlayerProfile.player.playerShips.Count > 0)
        {
            index++;
            shipName.text = PlayerProfile.player.playerShips[index].shipName;
        }
    }

    public void PreviousWeapon()
    {
        if (weapIndex > 0)
        {
            weapIndex--;
        }
    }
    public void NextWeapon()
    {
        if(weapIndex<PlayerProfile.player.weaponList.Count-1 && PlayerProfile.player.weaponList.Count > 0)
        {
            weapIndex++;
        }
    }
    public void PreviousPart()
    {
        if (partIndex > 0)
        {
            partIndex--;
        }
    }
    public void NextPart()
    {
        if (partIndex < PlayerProfile.player.partList.Count-1 && PlayerProfile.player.partList.Count > 0)
        {
            partIndex++;
        }
    }

    public void AddRemoveWeapon()
    {
        if (PlayerProfile.player.playerShips.Count > 0 && PlayerProfile.player.weaponList.Count > 0)
        {
            Weapon tempWeapon;
            tempWeapon = PlayerProfile.player.playerShips[index].weapon;
            PlayerProfile.player.playerShips[index].weapon = PlayerProfile.player.weaponList[weapIndex];//Replaces the current weapon on the ship with the one in the weapon list
            PlayerProfile.player.weaponList[weapIndex] = tempWeapon;
        }
    }

    public void AddRemovePart()
    {
        if(PlayerProfile.player.playerShips.Count > 0 && PlayerProfile.player.partList.Count > 0)//Checks that there are ships, and a part list
        {
            int spaces = PlayerProfile.player.playerShips[index].partLimit;
            for (int i = 0; i < PlayerProfile.player.playerShips[index].parts.Count && PlayerProfile.player.playerShips[index].parts.Count <= PlayerProfile.player.playerShips[index].partLimit; i++)//Uses the ships part limit to indicate how many parts to look at
            {
                if (PlayerProfile.player.playerShips[index].parts[i] != null)
                {
                    spaces--;
                    if (PlayerProfile.player.playerShips[index].parts[i] == PlayerProfile.player.partList[partIndex])//Checks if the part selected is already part of the ship
                    {
                        PlayerProfile.player.partList[partIndex].inUse = false;
                        PlayerProfile.player.playerShips[index].parts.RemoveAt(i);//If it is , it is removed
                        return;
                    }
                }
            }
            if (PlayerProfile.player.playerShips[index].parts.Count < PlayerProfile.player.playerShips[index].partLimit && PlayerProfile.player.partList[partIndex].inUse == false)//If the ship has less parts than the limit, add the part to it, if not in use
            {
                PlayerProfile.player.playerShips[index].parts.Add(PlayerProfile.player.partList[partIndex]);//Adds the part to the ships private list, sets the part status to in use, and ends the function
                PlayerProfile.player.partList[partIndex].inUse = true;
                return;
            }            
        }
    }
}
