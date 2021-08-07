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

    // game modes screen function
    public void GameModes()
    {
        SceneManager.LoadScene("Game Modes Screen");
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

    // start game function
    public void StartGame()
    {
        SceneManager.LoadScene("Single Player");
    }

    //start vs mode
    public void VSMode()
    {
        SceneManager.LoadScene("VS Mode");
    }
}
