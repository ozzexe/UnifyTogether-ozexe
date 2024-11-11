using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BallCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Rigidbody ballRigidbody;
    public float baseDistance = 10f;
    public float maxZoomOutDistance = 15f;
    public float zoomOutSpeed = 2f;

    private CinemachineTransposer transposer;

    private void Start()
    {
        if (virtualCamera != null)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = new Vector3(0, 5, -baseDistance);
        }
    }

    private void LateUpdate()
    {
        float speed = ballRigidbody.linearVelocity.magnitude;

        float targetDistance = Mathf.Lerp(baseDistance, maxZoomOutDistance, speed / zoomOutSpeed);
        transposer.m_FollowOffset = new Vector3(0, 5, -targetDistance);
    }
}
