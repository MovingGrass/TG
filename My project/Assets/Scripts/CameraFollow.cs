using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector2 offset; // Offset between the camera and the player

    void Update()
    {
        if (target != null)
        {
            // Update camera position based on player's position and offset
            Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y, transform.position.z);
            transform.position = targetPosition;
        }
    }
}
