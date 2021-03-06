﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 6.0F;
    public float turningSpeed = 800.0f;
    public float gravity = 14.0F;
    public float turnSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float verticalVelocity;

    private Vector3 movement = Vector3.zero;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveHorizontal = -Input.GetAxis("Vertical");
        float moveVertical = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (jump)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 verticalMovement = new Vector3(0, verticalVelocity, 0);

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Only Update the player's rotation if he's moving. This way we keep the rotation.
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Rotate();
        }
        controller.Move(movement * movementSpeed * Time.deltaTime);
        controller.Move(verticalMovement * Time.deltaTime);
    }

    void Rotate()
    {
        float step = turningSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement), step);
    }
}
