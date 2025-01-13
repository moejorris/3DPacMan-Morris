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

    // [SerializeField] float clydeRadius = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputCheck();
        UpdateIntendedDirection();
        CheckForWarp();
        rb.linearVelocity = vel * moveSpeed;
    }

    void Update()
    {
        AssignNewDirection();
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

    void AssignNewDirection()
    {
        if(nextDir == Vector3.zero) return;

        if(!Physics.CheckBox(transform.position + nextDir, transform.localScale * checkBoxSize, Quaternion.identity, checkLayerMask) && !Physics.Raycast(transform.position, nextDir, 0.6f, checkLayerMask) && nextDir != Vector3.zero)
        {
            Debug.Log("new dir assigned");
            vel = nextDir;
            meshTransform.forward = nextDir;
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
}
