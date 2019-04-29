using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Nicholas Easterby - EAS12337350
//This menu script handles every scene transition in  the game.
//The add scene function allows one scene to be overlaid upon the previous scene. It also removes the previous overlaid scene
//The ened battle function is called from within the battle
public class MenuScript : MonoBehaviour
{
    int heldScene;
    
    public void AddScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo, LoadSceneMode.Additive);//Adds a sselected scene on top of current scene
        
        if (heldScene >1)//If there is a previous scene
        {
            DeLoadScene(heldScene);
        }
        heldScene = sceneNo;//Sets the current scene as the stored previous scene
    }

    public void RemoveScene()
    {
        SceneManager.UnloadSceneAsync(4);
        heldScene = 0;
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

    void DeLoadScene(int held)
    {
        SceneManager.UnloadSceneAsync(held);//Removes the previously added scene
    }
}
