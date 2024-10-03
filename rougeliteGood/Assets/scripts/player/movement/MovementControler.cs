using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementControler : MonoBehaviour
{
    [SerializeField] int terminalVelocity;
    [SerializeField] float acceleration;
    bool grounded = true;
    [SerializeField] int walkSpeed;
    [SerializeField] int sprintSpeed;
    [SerializeField] Vector3 velocity;
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        int maxSpeed = new int();
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"));
        input.Normalize();
        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            maxSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            maxSpeed = walkSpeed;
        }
        else 
        {
            maxSpeed = terminalVelocity;
        }

        
        if (velocity.magnitude > maxSpeed && (input.magnitude == 1 || !grounded))
        {
            velocity -= velocity.normalized * acceleration * 2f * Time.deltaTime;
        }

        else if (velocity.magnitude > acceleration * Time.deltaTime && input.magnitude == 0 && grounded) 
        {
            velocity -= velocity.normalized * acceleration * Time.deltaTime;
        }

        else if (grounded && input.magnitude == 0) 
        {
            velocity = Vector3.zero;
        }

        else if (!grounded)
        {
            velocity -= velocity.normalized * 1.1f * Time.deltaTime;
        }
        velocity += transform.forward * input.z * acceleration * Time.deltaTime + transform.right * input.x * acceleration * Time.deltaTime;


        controller.Move(velocity * Time.deltaTime);

    }

    public void AddForce(Vector3 direction, float force)
    {
        velocity += direction * force;
    }
}
