using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

    public void DoCollide(Collider2D collision)
    {
        if (collision.tag == "Ship")
        {
            if (collision.gameObject.GetComponent<ShipControl>().parts.Count > 0)//Checks if ship has any parts
            {
                collision.gameObject.GetComponent<ShipControl>().parts.RemoveAt(0);//If it does, it remove one
            }
            else if (collision.gameObject.GetComponent<ShipControl>().parts.Count == 0)
            {
                Destroy(collision.gameObject, 0.5f);//If no parts, the explosion destroys the ship
            }
        }
        else if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject, 0.5f);//Destroys the enemy
        }
    }
}
