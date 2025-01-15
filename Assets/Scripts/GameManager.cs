//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region make public static
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public int lives = 3;

    public int score;

    public int pellets;
    public int pelletRequirement;


    [SerializeField] TextMeshProUGUI pelletText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;

    // public bool powerPellet;

    public void GotPellet()
    {
        pellets++;
        pelletText.text = "Pellets: " + pellets + "/" + pelletRequirement;
        //update Ui

        if(pellets >= pelletRequirement)
        {
            GhostManager.instance.StopGame();
            FindFirstObjectByType<PlayerMovement>().alive = false;


            endPanel.SetActive(true);
            winText.SetActive(true);

            SoundManager.Instance.Invoke("PlayLevelRestart", 0.25f);
        }
    }

    void Start()
    {
        endPanel.SetActive(false);
        winText.SetActive(false);
        loseText.SetActive(false);

        pelletText.text = "Pellets: " + pellets + "/" + pelletRequirement;
        livesText.text = "Lives: " + lives;

        SoundManager.Instance.PlayLevelStart();
        Invoke("StartGameplay", 4.2f);
    }

    void Restart()
    {
        SoundManager.Instance.PlayLevelRestart();
        GhostManager.instance.Reset();
        FindFirstObjectByType<PlayerMovement>().Reset();

        Invoke("StartGameplay", 5f);
    }

    void StartGameplay()
    {
        FindFirstObjectByType<PlayerMovement>().alive = true;
        GhostManager.instance.StartGame();
    }

    public void Death()
    {
        GhostManager.instance.StopGame();
        SoundManager.Instance.PlayDeath();
        lives--;

        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            //game over
            Debug.Log("no more lives");
            endPanel.SetActive(true);
            loseText.SetActive(true);
        }
        else
        {
            Invoke("Restart", 4f);
        }
    }
    
}
