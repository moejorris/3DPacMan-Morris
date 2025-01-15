//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using Unity.VisualScripting;
using UnityEngine;

public class PelletSpawner : MonoBehaviour
{
    [SerializeField] LayerMask checkMask;
    [SerializeField] float levelRangeX = 14.5f;
    [SerializeField] float levelRangeY = 7.5f;

    [SerializeField] GameObject pelletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPellets();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void SpawnPellets()
    {
        int pelletCount = 0;
        Vector3 checkPos = new Vector3(0, 10f, 0);
        RaycastHit rayHit;
        for(float z = levelRangeY; z >= -levelRangeY; z -= 0.5f)
        {
            for(float x = -levelRangeX; x <= levelRangeX; x += 0.5f)
            {
                checkPos.x = x;
                checkPos.z = z;
                Vector3 spawnPos = new Vector3(x, 0, z);
                Physics.Raycast(checkPos, Vector3.down, out rayHit, 100f, checkMask, QueryTriggerInteraction.Collide);
                // Debug.DrawLine(checkPos, rayHit.point, Color.red, 1000f);
                
                if(rayHit.collider.name == "Floor" && !Physics.CheckSphere(spawnPos, 0.3f))
                {
                    Instantiate(pelletPrefab, new Vector3(x, 0, z), Quaternion.identity).transform.parent = transform;
                    pelletCount++;
                }
            }
        }
        Debug.Log("There are " + pelletCount + "pellets in the scene");

        if(GameManager.Instance == null) return;
        GameManager.Instance.pelletRequirement = pelletCount;
    }
}
