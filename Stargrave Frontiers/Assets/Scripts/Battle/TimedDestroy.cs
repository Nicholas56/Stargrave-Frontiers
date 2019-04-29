using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This script destroys any instance it is attached to within the time limit
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
