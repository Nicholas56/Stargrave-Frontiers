using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Vector2 start;

    public float distance;
    float travel;

    public GameObject explosion;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();//Gets the rigidbody component from the object
	}
	
	// Update is called once per frame
	void Update ()
    {
        rigid.AddForce(direction*speed, ForceMode2D.Impulse);//Adds force to the projectile in the direction given

        travel = Vector2.Distance(start, transform.position);//Calculates how far the projectile has travelled
        if(travel >=distance)
        {
            Instantiate(explosion, transform);//When it has gone far enough, it self-destructs 
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosion, transform);//When it hits something, it detonates
        Destroy(gameObject);
    }
}
