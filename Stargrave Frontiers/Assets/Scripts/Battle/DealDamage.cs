using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

    public void DoCollide(Collider2D collision)
    {
        if (collision.tag == "Ship")
        {
            if (collision.gameObject.GetComponent<ShipControl>().ship.parts.Count > 0)//Checks if ship has any parts
            {
                collision.gameObject.GetComponent<ShipControl>().ship.parts.RemoveAt(0);//If it does, it remove one
            }
            else if (collision.gameObject.GetComponent<ShipControl>().ship.parts.Count == 0)
            {
                PlayerProfile.RemoveShip(collision.GetComponent<ShipControl>().shipId);//When your ship is destroyed, removes it from the profile
                Destroy(collision.gameObject, 0.5f);//If no parts, the explosion destroys the ship
            }
        }
        else if (collision.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<EnemyAI>().hitPoints > 1)
            {
                collision.gameObject.GetComponent<EnemyAI>().hitPoints--;
            }
            else
            {
                Destroy(collision.gameObject, 0.5f);
                PlayerProfile.money += 50;
                PlayerProfile.score.currentEarnings += 50;
            }//Destroys the enemy
        }
    }
}
