using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    //public Rigidbody rb;
    public VariableJoystick variableJoystick;
    public float JumpForce;
    public static PlayerMovement instancePlayerMovement;
    float DisToGround;
    public Collider PlayerCol;
    public CharacterController controller;
    Vector3 moveDirection;
    public float gravityScale;
    // Start is called before the first frame update
    void Start()
    {
        if (instancePlayerMovement == null)
        {
            instancePlayerMovement = this;
        }

        // DisToGround = PlayerCol.bounds.extents.y;
        controller = GetComponent<CharacterController>();
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, DisToGround + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = new Vector3(variableJoystick.Horizontal * movementSpeed, rb.velocity.y, variableJoystick.Vertical * movementSpeed);
        // Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        //  rb.AddForce(direction * movementSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        moveDirection = new Vector3(variableJoystick.Horizontal * movementSpeed, moveDirection.y, variableJoystick.Vertical * movementSpeed);
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

    }
    public void PlayerJump()
    {
        // rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);
        if (controller.isGrounded)
        {
            moveDirection.y = JumpForce;
        }
        Debug.Log("Loncat");


    }
}
