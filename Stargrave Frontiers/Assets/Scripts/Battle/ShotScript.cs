using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This script handles the motion of the shot fired from the ship. This uses the data about the weapons to determine the properties of the shot.
//This also instantiates the explosion animation.
public class ShotScript : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Vector2 start;

    public float distance;
    float travel;
    public bool hasHit;

    public GameObject explosion;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();//Gets the rigidbody component from the object
        hasHit = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!hasHit)//Checks if the shot has hit anything
        {
            rigid.AddForce(direction * speed, ForceMode2D.Force);//Adds force to the projectile in the direction given

            travel = Vector2.Distance(start, transform.position);//Calculates how far the projectile has travelled
            if (travel >= distance)
            {
                Instantiate(explosion, transform);//When it has gone far enough, it self-destructs 
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHit)
        {
            GetComponent<DealDamage>().DoCollide(collision);
            hasHit = true;
            GameObject newObject = Instantiate(explosion, transform);//When it hits something, it detonates
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;//Stops the parent shot from moving
            newObject.transform.position = transform.position;//Aligns the explosion with the hit
            newObject.transform.localScale *= 4;//Makes the explosion larger
            Invoke("SelfDestruct", 0.2f);//Destroys the parent shot
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
