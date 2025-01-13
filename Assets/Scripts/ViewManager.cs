using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] Camera cam3d;
    [SerializeField] Camera cam2d;

    void Start()
    {
        cam3d.enabled = false;
        cam2d.enabled = true;
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
        if(cam2d.enabled)
        {
            cam2d.enabled = false;
            cam3d.enabled = true;
        }
        else
        {
            cam3d.enabled = false;
            cam2d.enabled = true;
        }
    }
}
