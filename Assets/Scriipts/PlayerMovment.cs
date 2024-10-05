using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed;
    public Transform PlayerOrientation;
    public float HorizontalInput;
    public float VerticalInput;
    Vector3 moveDir;
    Rigidbody rigBody;
    public float playerHeight;
    public LayerMask IsGround;
    public bool Grounded;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float InAirMultiplyer;
    public bool readyToJump;
    public bool isrunning;
    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
        rigBody.freezeRotation = true;
        readyToJump = true;
    }

    void Update()
    {
        //ground check
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, IsGround);  

        input();
        speedControl();
        //apply drag
        if(Grounded) 
        {
            rigBody.drag = groundDrag;
        }
        else
        {
            rigBody.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }
    private void input()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) && readyToJump && Grounded) 
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isrunning = true;
        }
        else
        {
            isrunning = false;  
        }
    }

    private void movePlayer()
    {
        //calcualtion so you always move in the dorection you are looking
        moveDir = PlayerOrientation.forward * VerticalInput + PlayerOrientation.right * HorizontalInput;

        isrunning = false;
        if(Grounded)
        {
            rigBody.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
           
        }
        else if (isrunning)
        {
            isrunning = true;

            rigBody.AddForce(moveDir.normalized * (moveSpeed * 2) * 10f, ForceMode.Force);
        }
        else
        {
            rigBody.AddForce(moveDir.normalized * moveSpeed * 10f * InAirMultiplyer , ForceMode.Force);
        }
   

    }

    //this function handles if the player charicter goes faster then the move speed to limit the acceleration
    private void speedControl()
    {
        Vector3 flatVelocity = new Vector3(rigBody.velocity.x, 0f, rigBody.velocity.z);

        if(flatVelocity.magnitude>moveSpeed)
        {
            if(isrunning) 
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed * 2;
                rigBody.velocity = new Vector3(limitedVelocity.x, rigBody.velocity.y, limitedVelocity.z);
            }
            else
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rigBody.velocity = new Vector3(limitedVelocity.x, rigBody.velocity.y, limitedVelocity.z);
            }
           
        }
        
    }

    private void Jump()
    {
        //set y velocity to 0 to always jump the same height
        rigBody.velocity = new Vector3(rigBody.velocity.x, 0f, rigBody.velocity.z);

        rigBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);//use impulse because you only apply the force once

    }

    private void resetJump()
    {
        readyToJump = true;
    }
}
