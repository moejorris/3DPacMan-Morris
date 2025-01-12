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

    public bool powerPellet;




    public void GotPellet()
    {
        pellets++;

        //update Ui
    }
}
