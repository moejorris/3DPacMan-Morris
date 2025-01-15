//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform meshTransform;
    [SerializeField] float moveSpeed = 4f;
    Rigidbody rb;
    Vector3 nextDir;
    Vector3 input;

    [SerializeField] float checkBoxSize = 0.4f;
    [SerializeField] LayerMask checkLayerMask;
    Vector3 vel;
    Vector3 startPos;
    [HideInInspector] public bool alive = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        nextDir = Vector3.right;
        Reset();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!alive)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        InputCheck();
        UpdateIntendedDirection();
        CheckForWarp();
        transform.forward = vel;
        rb.linearVelocity = vel * moveSpeed;
    }

    void Update()
    {
        AssignNewDirection();
    }

    void LateUpdate()
    {
        AnimCheck();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, vel);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + nextDir, transform.localScale * (checkBoxSize * 2f));
        // Gizmos.DrawRay(transform.position, transform.right * 2f);

        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(transform.position, clydeRadius);
    }



    void InputCheck()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void UpdateIntendedDirection()
    {
        if(input.x != 0 ^ input.z != 0)
        {
            nextDir = input.normalized;
        }
    }

    void AnimCheck()
    {
        if(!Physics.Raycast(transform.position, vel, 0.6f, checkLayerMask) || !alive)
        {
            GetComponent<PlayerAnimation>().notMoving = false;
        }
        else
        {
            GetComponent<PlayerAnimation>().notMoving = true;
        }
    }

    void AssignNewDirection()
    {
        if(nextDir == Vector3.zero) return;

        if(!Physics.CheckBox(transform.position + nextDir, transform.localScale * checkBoxSize, Quaternion.identity, checkLayerMask) && !Physics.Raycast(transform.position, nextDir, 0.6f, checkLayerMask) && nextDir != Vector3.zero)
        {
            Debug.Log("new dir assigned");
            vel = nextDir;
            nextDir = Vector3.zero;
        }
    }

    void CheckForWarp()
    {
        float xPos = transform.position.x;

        if(Mathf.Abs(xPos) > 17.5 && Mathf.Sign(xPos) == Mathf.Sign(vel.x))
        {
            transform.position = new Vector3(-xPos, 0, 0.5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Ghost>())
        {
            if(!alive) return;
            alive = false;
            Debug.Log("Death!");
            GameManager.Instance.Death();
        }
    }

    public void Reset()
    {
        alive = false;
        transform.position = startPos;
        transform.forward = Vector3.right;
        nextDir = transform.forward;
        vel = transform.forward;
    }
}
