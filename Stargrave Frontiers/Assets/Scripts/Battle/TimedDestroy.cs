using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {

    public float timeLimit;
    // Use this for initialization
    void Start()
    {
        Invoke("killUnit", timeLimit);
    }

    // Update is called once per frame
    void killUnit()
    {
        Destroy(gameObject);
    }
}
