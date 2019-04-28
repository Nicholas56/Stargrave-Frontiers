using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    int heldScene;
    
    public void AddScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo, LoadSceneMode.Additive);//Adds a sselected scene on top of current scene
        if (heldScene >1)//If there is a previous scene
        {
            SceneManager.UnloadSceneAsync(heldScene);//Removes the previously added scene
        }
        heldScene = sceneNo;//Sets the current scene as the stored previous scene
    }

    public void LoadNextScene(int sceneIndex)
    {
        if(sceneIndex == 10) { PlayerProfile.PlayMusic(); }
        SceneManager.LoadScene(sceneIndex);//Loads the scene according to the index given
    }

    public void ExitProgram()//Quits the program
    {
        Application.Quit();
    }

    public static void EndBattle()
    {
        MapGenerator.isBattle = false;
        
        SceneManager.LoadScene(11);
    }
}
