using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static Profile player;

    public static int money;
    public static Score score;

    public static List<Ship> gameShips;
    public static List<Part> gameParts;
    public static List<Weapon> gameWeapons;
    public static List<Enemy> gameEnemies;

    public static Vector2 mapSize;
    public static int enemyNumber;
    public static int obstacleNumber;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);//Makes sure the profile is available in later scenes
        gameWeapons = new List<Weapon>//Makes a list of available weapons
        {
            new Weapon("Advanced Cannon", 31, 10f, 6f, 50),new Weapon("Basic Laser", 31, 5f, 10f, 50),new Weapon("Advanced Laser", 31, 10f, 10f, 80),new Weapon("Basic Cannon", 31, 5f, 6f, 30)
        };
        gameParts = new List<Part>//Makes a list of available parts
        {
            new Part("Hull", 30), new Part("Thermals", 30), new Part("Control System", 30), new Part("Hydrazine Thrusters", 30), new Part("Navigation", 30)
        };
        gameShips = new List<Ship>();
        gameShips.Add(new Ship("Basic Ship", 310, new Weapon("Basic Cannon", 31, 5f, 6f, 30), 200,4));//Adds a ship to the ship list
        gameEnemies = new List<Enemy>//Makes a list of available enemies
        {
            new Enemy("Frigate", 310, 6.5f, 10f, 1), new Enemy("Cruiser", 310, 8, 10, 2)
        };

        player = new Profile();//Creates the new profile for the player
        money = 1000;
        RandomBattle();//Pre-randomises the battle parameters
        player.enemyTypes.Add(gameEnemies[0]);
        player.enemyTypes.Add(gameEnemies[1]);//Adds the type of enemies to appear in game
        score = new Score();
    }

    static Ship MakeNewShip(int item)//Creates a new instance of the catalogue item
    {
        Ship madeShip = new Ship(gameShips[item].shipName, gameShips[item].actionPoints, gameShips[item].weapon, gameShips[item].sCost, gameShips[item].partLimit);
        return madeShip;
    }
    static Weapon MakeNewWeapon(int item)//Creates a new instance of the catalogue item
    {
        Weapon madeWeapon = new Weapon(gameWeapons[item].weaponName, gameWeapons[item].actionCost, gameWeapons[item].shotRange, gameWeapons[item].accuracy, gameWeapons[item].wCost);
        return madeWeapon;
    }
    static Part MakeNewPart(int item)//Creates a new instance of the catalogue item
    {
        Part madePart = new Part(gameParts[item].partName, gameParts[item].pCost);
        return madePart;
    }
    static Enemy MakeNewEnemy(int item)//Creates a new instance of the catalogue item
    {
        Enemy madeEnemy = new Enemy(gameEnemies[item].typeName, gameEnemies[item].actionPoints, gameEnemies[item].range, gameEnemies[item].accuracy, gameEnemies[item].hitPoints);
        return madeEnemy;
    }

    public static void PurchaseWeapon(int weaponListItem)//Chose an item from the weapon list to add to the player profile
    {
        if (money >= gameWeapons[weaponListItem].wCost)
        {
            
            player.weaponList.Add(MakeNewWeapon(weaponListItem));
            money -= gameWeapons[weaponListItem].wCost;
        }
    }

    public static void PurchasePart(int partListItem)//Chose an item from the part list to add to the player profile
    {
        if (money >= gameParts[partListItem].pCost)
        {
            player.partList.Add(MakeNewPart(partListItem));
            money -= gameParts[partListItem].pCost;
        }
    }

    public static void PurchaseShip(int shipListItem)//Chose an item from the ship list to add to the player profile
    {
        if (money >= gameShips[shipListItem].sCost && player.playerShips.Count<10)
        {
            
            player.playerShips.Add(MakeNewShip(shipListItem));
            money -= gameShips[shipListItem].sCost;
        }
    }

    public static void SellWeapon(int weaponListItem)
    {
        if (player.weaponList.Count > 0)
        {
            money += player.weaponList[weaponListItem].wCost / 2;//Lets you sell a weapon back for half the price
            player.weaponList.RemoveAt(weaponListItem);
        }
    }

    public static void SellPart(int partListItem)
    {
        if (player.partList.Count > 0)
        {
            money += player.partList[partListItem].pCost / 2;//Lets you sell a part back for half the price
            player.partList.RemoveAt(partListItem);
        }
    }

    public static void SellShip(int shipListItem)
    {
        if (player.playerShips.Count > 0)
        {
            money += player.playerShips[shipListItem].sCost / 2;//Lets you sell a ship back for half the price
            player.playerShips.RemoveAt(shipListItem);
        }
    }

    public static void RandomBattle()
    {
        int xNum = Random.Range(20, 30);
        int yNum = Random.Range(20, 30);//Pick random coordinates for the map size
        if (xNum % 2 == 0) { xNum++; }
        if (yNum % 2 == 0) { xNum++; }//Ensures that the numbers are odd numbers, for the cursor to work
        mapSize = new Vector2(xNum, yNum);
        enemyNumber = Random.Range(5, 20);
        obstacleNumber = Random.Range(50, 120);//Random number of enemies and obstacles
    }
}
