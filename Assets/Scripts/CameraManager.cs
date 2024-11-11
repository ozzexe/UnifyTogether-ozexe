using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera combinedCamera;

    private void Start()
    {
        SetCameras(false);
    }

    public void SetCameras(bool isCombined)
    {
        Debug.Log("SetCameras called with isCombined: " + isCombined);
        if (isCombined)
        {
            camera1.enabled = false;
            camera2.enabled = false;
            combinedCamera.enabled = true;
        }
        else
        {
            camera1.enabled = true;
            camera2.enabled = true;
            combinedCamera.enabled = false;
        }
        Debug.Log("Camera states - Camera1: " + camera1.enabled + ", Camera2: " + camera2.enabled + ", CombinedCamera: " + combinedCamera.enabled);
    }

}
