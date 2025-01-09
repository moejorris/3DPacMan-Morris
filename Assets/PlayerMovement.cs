using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    Rigidbody rb;
    Vector3 curDir;
    Vector3 nextDir;
    Vector3 input;


    Vector3 vel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
        // UpdateIntendedDirection();

        if(input.x != 0)
        {
            vel.x = Mathf.Sign(input.x);
        }
        if(input.y != 0)
        {
            vel.z = Mathf.Sign(input.y);
        }

        rb.linearVelocity = vel * moveSpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, curDir);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, nextDir);
    }



    void InputCheck()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void UpdateIntendedDirection()
    {
        if(input != Vector3.zero)
        {
            nextDir = input;
        }
        if(nextDir == curDir)
        {
            nextDir = Vector3.zero;
        }
    }
}
