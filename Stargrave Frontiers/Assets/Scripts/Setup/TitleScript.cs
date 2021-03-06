﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Nicholas Easterby - EAS12337350
//This script is for the opening scene to the game. This oscilates the color for the opening graphic
//Also handles the player input that allows the player to proceed to the next scene, while the graphic fades to black.
public class TitleScript : MonoBehaviour
{
    float startTime;
    public float duration;
    Color startColor = Color.white;
    Color endColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t2 = (Mathf.Cos(((Time.time - startTime) + duration) * Mathf.PI / duration) + 1) * 0.5f;

    // Use t2 instead of t1 if you want smoother interpolation
        GetComponent<Image>().color = Color.Lerp(startColor, endColor, t2);

        if (Input.anyKey)
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        startColor = GetComponent<Image>().color;
        duration = 4;
        startTime = Time.time;
        endColor = Color.black;
        Invoke("LoadGame",4);
    }

    void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

}
