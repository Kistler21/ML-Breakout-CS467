using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Code derived from https://www.youtube.com/watch?v=NWG8vO02oj4

public class Start_Menu : MonoBehaviour
{
    // Function to force quit the game for "quit" button
    public void QuitGame()
    {
        Application.Quit();
        // debug log statement for testing. Force quit doesn't work when testing game within Unity window. 
        Debug.Log("Quit button pushed!");
    }

    // start game function
    public void StartGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    // function to go to instructions scene
    public void InstructionsScene()
    {
        SceneManager.LoadScene("Instructions Scene");
    }

    // function to go Main Menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
