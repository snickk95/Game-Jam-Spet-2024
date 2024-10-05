using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //look sensitivity
    public float sensX;
    public float sensY;

    public Transform playerOrientation;
    public Transform cameraOrientation;
    
    //x and y rotations
    public float xRot;
    public float yRot;

    void Start()
    {
        //lock cursser make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRot += mouseX;
        xRot -= mouseY;

        //prevent looking up or down 90 degrees 
        xRot = Mathf.Clamp(xRot, -90, 90);

        //apply rotation
        cameraOrientation.rotation = Quaternion.Euler(xRot, yRot, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
