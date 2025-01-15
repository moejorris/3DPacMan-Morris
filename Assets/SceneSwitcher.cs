//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenuMorris");
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene("HelpMorris");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MazeMorris");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
