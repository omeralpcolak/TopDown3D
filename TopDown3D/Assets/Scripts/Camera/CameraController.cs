using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffSet; 

    public float smoothSpeed = 0.125f;

    private Vector3 originalOffset; // Store the original offset
    private Vector3 newOffset; // Store the new offset for specific points
    private bool useNewOffset = false; // Flag to determine if you should use the new offset

    private void Start()
    {
        originalOffset = cameraOffSet;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 currentOffset = useNewOffset ? newOffset : originalOffset;
            Vector3 desiredPosition = player.position + currentOffset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(player);
        }
    }

    public void ChangeOffset(Vector3 newOffset)
    {
        this.newOffset = newOffset;
        useNewOffset = true;
    }

    public void ResetOffset()
    {
        useNewOffset = false;
    }
}
