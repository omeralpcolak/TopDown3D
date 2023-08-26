using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset from the player

    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    private void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = player.position + offset;

        // Use Lerp to smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;

        // Make the camera look at the player
        transform.LookAt(player);
    }
}
