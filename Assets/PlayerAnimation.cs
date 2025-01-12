using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particleSystem;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(rb.linearVelocity.magnitude < 1)
        {
            animator.speed = 0;
            particleSystem.Stop();
        }
        else
        {
            animator.speed = 1;
            particleSystem.Play();
        }
    }
}
