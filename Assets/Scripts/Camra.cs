using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Camra : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 180;

    float totalRotX = 0;
    float totalRotY = 0;

    [SerializeField] Rigidbody playerRigidbody;

    public bool lockCursor = true;

    float xRotation;
    float yRotation;

    void Update()
    {
        CalculateRotation();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void FixedUpdate()
    {
        ApplyRotation();
    }

    void CalculateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        totalRotY += mouseY;
        totalRotX += mouseX;
    }

    void ApplyRotation()
    {
        Camera.main.transform.localRotation = Quaternion.Euler(-totalRotY, 0, 0);
        playerRigidbody.rotation = Quaternion.Euler(0, totalRotX, 0);

        /*
        xRotation = Mathf.Clamp(xRotation, 0f, 30);

        playerRigidbody.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        */
    }
}
