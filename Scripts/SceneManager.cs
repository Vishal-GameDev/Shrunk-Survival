using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Load a scene by name
    public void Game()
    {
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void CutScene()
    {
        SceneManager.LoadScene(4);
    }
    public void WIN()
    {
        SceneManager.LoadScene(2);
    }



    // Restart the current scene
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit the game (only works in build mode)
    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }
}
