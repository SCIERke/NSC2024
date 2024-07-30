using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public float sensitivity = 15f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private bool toggleMouse = false;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize rotation values based on the current transform rotation
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        rotationY = currentRotation.y;
        rotationX = currentRotation.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            toggleMouse = !toggleMouse;
            Cursor.lockState = toggleMouse ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = toggleMouse;
        }
        if (!toggleMouse)
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            // Update rotation values
            rotationY += mouseX;
            rotationX -= mouseY;

            // Clamp the vertical rotation to prevent flipping
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            // Apply the rotations to the transform
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }
}
