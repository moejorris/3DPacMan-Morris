//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using System.Collections;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    #region public static set
    public static GhostManager instance;
    void Awake(){instance = this;}
    #endregion


    bool updateGhosts = true;
    bool chase = false;
    float scatterTime = 7f;
    float chaseTime = 20f;

    [SerializeField] float ghostUpdateInterval = 0.2f;

    [SerializeField]Ghost[] ghosts;

    void Start()
    {
        ghosts = FindObjectsByType<Ghost>(FindObjectsSortMode.None);
    }

    public void Reset()
    {
        ResetGhosts();
    }

    public void StartGame()
    {
        updateGhosts = true;
        ResetGhosts();
        StartCoroutine(UpdateGhosts());
        StartCoroutine(TimingStates());
    }

    public void StopGame()
    {
        updateGhosts = false;
        StopGhosts();
    }

    IEnumerator UpdateGhosts()
    {
        while(updateGhosts)
        {
            for(int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].UpdateDestination();

                // Debug.Log(ghosts[i].name + " updated");
            }

            yield return new WaitForSeconds(ghostUpdateInterval);        
        }
    }

    IEnumerator TimingStates()
    {
        while(updateGhosts)
        {
            if(chase == false)
            {
                yield return new WaitForSeconds(scatterTime);
                chase = true;
                UpdateStates();
            }
            else
            {
                yield return new WaitForSeconds(chaseTime);
                chase = false;
                UpdateStates();
            }
        }
    }

    void UpdateStates()
    {
        for(int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].UpdateState(chase);
        }
    }

    void StopGhosts()
    {
        for(int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].Stop();
        }
    }

    void ResetGhosts()
    {
        for(int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].Reset();
        }
    }
}
