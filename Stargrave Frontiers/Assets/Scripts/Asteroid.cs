using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite altSprite;
    public float rotation;
    Transform fog;

    // Start is called before the first frame update
    void Start()
    {
        int spriteNum = Random.Range(0, 1);
        if (spriteNum == 1) { GetComponent<SpriteRenderer>().sprite = altSprite; }
        float rotNum = Random.Range(-1000, 1000);
        rotation = rotNum / 1000;
        fog = transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fog.Rotate(Vector3.forward, rotation);
        //float currentAngle = transform.rotation.eulerAngles.z;
        //transform.eulerAngles = new Vector3(0, 0, currentAngle+rotation);
    }
}
