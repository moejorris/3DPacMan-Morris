using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    enum GhostType {blinky, pinky, inky, clyde};
    enum GhostState {idle, patrol, chase, frightened, eaten};

    [SerializeField] GhostType ghostType;
    GhostState currentState;


    void ChooseTarget()
    {
        if(ghostType == GhostType.blinky)
        {
            //target pos == pacman pos
        }
        else if(ghostType == GhostType.pinky)
        {
            //target pos = pacman pos + 4 tiles pacman forward
        }
        else if(ghostType == GhostType.inky)
        {
            // 2 tiles in front of pacman
            //draw line from there to blinky
            //rotate 180 deg with 2 tiles in front of pacman as origin... definitely going be a lot of trial and error with this one
        }
        else if(ghostType == GhostType.clyde)
        {
            //if distance from pacman greater than or equal to 8 tiles away (8 tiles = 4 unity units)
            // target = pacman pos
            //else target = scatter pos
        }
    }

}
