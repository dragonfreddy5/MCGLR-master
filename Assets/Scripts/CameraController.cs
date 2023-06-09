using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera variables
    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float mouseSensitivity = 100.0f;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    public bool cameraFree = true;
    public bool respawn = false;

    public static CameraController Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void LateUpdate () 
    {
        // Check if right mouse button is held down
        if (Input.GetMouseButton(1) && cameraFree)
        {
            // Get the mouse movement axes
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the camera based on the mouse movement
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
            yRotation += mouseX;

            Vector3 rotation = new Vector3(xRotation, yRotation, 0.0f);
            transform.rotation = Quaternion.Euler(rotation);
        }
        else if(!cameraFree)
        {
            transform.LookAt(target);
        }
        else if(respawn)
        {
            Vector3 rotation = new Vector3(xRotation, yRotation, 0.0f);
            transform.rotation = Quaternion.Euler(rotation);
        }

        Vector3 relTargetPos = new Vector3(target.position.x, target.position.y-1, target.position.z);

        // Update the camera position and rotation
        transform.position = relTargetPos - transform.forward * distance + Vector3.up * height;
    }
}
