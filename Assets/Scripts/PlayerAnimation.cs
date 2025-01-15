//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particleSys;

    public bool notMoving;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(notMoving || rb.linearVelocity.magnitude < 1)
        {
            animator.speed = 0;
            particleSys.Stop();
        }
        else
        {
            animator.speed = 1;
            particleSys.Play();
        }
    }
}
