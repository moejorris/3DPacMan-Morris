//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using UnityEngine;

public class Pellet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.GotPellet();
            SoundManager.Instance.PlayPellet();
            Destroy(gameObject);
        }
    }
}
