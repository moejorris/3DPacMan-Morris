//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    NavMeshAgent nav;

    public enum GhostType {blinky, pinky, inky, clyde};
    enum GhostState {scatter, chase, frightened, eaten};

    public GhostType ghostType;
    GhostState currentState;

    Vector3 ghostHousePos = new Vector3(0, 0, 0.5f);
    
    // chase
    Transform pacMan; //to keep track of pac-mans position and direction
    Transform blinky; //inky is dependant on blinky's position in order to flank pacman relative to blinky
    bool clydeFlee; //used to force clyde into scatter when he is too close to pac-man, like the original game

    // scatter
    [SerializeField] Transform scatterParent;
    List<Vector3> scatterPoints = new List<Vector3>();
    int currentScatterPoint;


    Vector3 startPos;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        pacMan = FindAnyObjectByType<PlayerMovement>().transform;
        currentState = GhostState.scatter;

        GetScatterPoints();
        FindBlinky();
    }
    

    void GetScatterPoints()
    {
        for(int i = 0; i < scatterParent.childCount; i++)
        {
            scatterPoints.Add(scatterParent.GetChild(i).position);
        }
    }

    void FindBlinky()
    {
        if(ghostType == GhostType.inky)
        {
            Ghost[] ghosts = FindObjectsByType<Ghost>(FindObjectsSortMode.None);

            for(int i = 0; i < ghosts.Length; i++)
            {
                if(ghosts[i].ghostType == GhostType.blinky)
                {
                    blinky = ghosts[i].transform;
                }
            }
        }
    }

    void ChooseTarget()
    {
        // PowerPelletOnly(); //not necessary for this part of the assignment
        ChaseMode();
        ScatterMode();
    }

    public void Reset()
    {
        transform.position = startPos;
        nav.Warp(startPos);
        nav.ResetPath();
    }

    public void Stop()
    {
        nav.ResetPath();
        nav.isStopped = true;
    }

    public void UpdateState(bool chasing)
    {
        if(chasing)
        {
            currentState = GhostState.chase;
        }
        else
        {
            currentState = GhostState.scatter;
        }
    }

    public void UpdateDestination()
    {
        ChooseTarget();
    }

    void PowerPelletOnly() //commented out of the loop because power pellets are not supposed to be implemented for part 1 of this assignment
    {
        if(currentState == GhostState.eaten)
        {
            nav.SetDestination(ghostHousePos);
        }
        else if(currentState == GhostState.frightened)
        {
            //pick random direction
            int rand = Random.Range(0, 4);

            switch(rand)
            {
                case 0:
                nav.SetDestination(transform.forward);
                break;

                case 1:
                nav.SetDestination(transform.right);
                break;

                case 2:
                nav.SetDestination(-transform.right);
                break;

                case 3:
                nav.SetDestination(-transform.forward);
                break;
            }
        }
    }

    void ChaseMode()
    {
        if(currentState != GhostState.chase) return;

        //like the original pac-man, each ghost has a different chase pattern.

        if(ghostType == GhostType.blinky)
        {
            nav.SetDestination(pacMan.position);
        }
        else if(ghostType == GhostType.pinky)
        {
            //target pos = pacman pos + 4 tiles pacman forward
            nav.SetDestination(pacMan.position + (pacMan.forward * 2f));
        }
        else if(ghostType == GhostType.inky)
        {
            // 2 tiles in front of pacman
            //draw line from there to blinky
            //rotate 180 deg with 2 tiles in front of pacman as origin... definitely going be a lot of trial and error with this one
            if(blinky != null)
            {
                Vector3 startPosition = pacMan.position + pacMan.forward;
                Vector3 endPosition = startPosition - (blinky.position - startPosition);
                nav.SetDestination(endPosition);
            }
            else
            {
                nav.SetDestination(pacMan.position);
            }
        }
        else if(ghostType == GhostType.clyde)
        {
            //if distance from pacman greater than or equal to 8 tiles away (8 tiles = 4 unity units)
            // target = pacman pos
            //else target = scatter pos

            if(Vector3.Distance(pacMan.position, transform.position) >= 4)
            {
                nav.SetDestination(pacMan.position);
            }
            else
            {
                //scatter destination
                clydeFlee = true;
            }
        }
    }

    void ScatterMode()
    {
        if(currentState != GhostState.scatter && ghostType != GhostType.clyde)
        {
            return;
        }

        if(ghostType == GhostType.clyde)
        {
            if(clydeFlee)
            {
                if(Vector3.Distance(transform.position, pacMan.position) > 4)
                {
                    clydeFlee = false;
                    return;
                }
            }
            else if(currentState != GhostState.scatter)
            {
                return;
            }
        }


        if(nav.remainingDistance <= 0.5f)
        {
            currentScatterPoint++;
            if(currentScatterPoint >= scatterPoints.Count)
            {
                currentScatterPoint = 0;
            }
        }

        nav.SetDestination(scatterPoints[currentScatterPoint]);

    }

}
