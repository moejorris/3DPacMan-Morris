//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] Camera cam3d;
    [SerializeField] Camera cam2d;

    void Start()
    {
        cam3d.enabled = true;
        cam2d.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Toggle2D();
        }
    }
    void Toggle2D()
    {
        cam2d.enabled = !cam2d.enabled;
        cam3d.enabled = !cam3d.enabled;
    }
}
