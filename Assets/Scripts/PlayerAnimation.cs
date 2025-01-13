using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particleSys;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(rb.linearVelocity.magnitude < 1)
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
