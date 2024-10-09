using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementControler : MonoBehaviour
{
    [SerializeField] int terminalVelocity;
    [SerializeField] float acceleration;
    [SerializeField] bool grounded = true;
    [SerializeField] LayerMask layerMask;
    [SerializeField] int walkSpeed;
    [SerializeField] int sprintSpeed;
    [SerializeField] float gravity;
    [SerializeField] Vector3 velocity;
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference sprint;
    CharacterController controller;
    [SerializeField] float moveSpeed;
    void Start()
    {
        moveSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
    }

    void OnEnable() 
    {
        sprint.action.started += StartSprint;
        sprint.action.canceled += StopSprint;
        jump.action.started += Jump;
        jump.action.canceled += StopJump;
    }

    void OnDisable() 
    {
        sprint.action.started -= StartSprint;
        sprint.action.canceled -= StopSprint;
        jump.action.started -= Jump;
        jump.action.canceled -= StopJump;
    }

    void Update()
    {
        if(Physics.CheckSphere(new Vector3(0, -0.76f, 0) + transform.position, 0.5f, layerMask) && velocity.y <= 0)
        {
            grounded = true;
            velocity.y = 0;
        }
        else 
        {
            grounded = false;
            velocity.y -= gravity * Time.deltaTime;
        }

        Vector2 input = move.action.ReadValue<Vector2>();

        input *= moveSpeed;

        velocity = transform.forward * input.y + transform.right * input.x + transform.up * velocity.y;

        controller.Move(velocity * Time.deltaTime);

    }

    public void AddForce(Vector3 direction, float force)
    {
        velocity += direction * force;
    }

    void StartSprint(InputAction.CallbackContext obj)
    {
        moveSpeed = sprintSpeed;
    }
    void StopSprint(InputAction.CallbackContext obj)
    {
        moveSpeed = walkSpeed;
    }
    void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("jump" + grounded);
        if(grounded)
        {
            Debug.Log("do dis");
            velocity.y = 10;
        }
    }
    void StopJump(InputAction.CallbackContext obj)
    {
        if (velocity.y > 0)
        {
            velocity.y /= 1.3f;
        }
    }
}
