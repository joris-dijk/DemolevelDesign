using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 100f;
    public Transform cameraTransform;

    private float xRotation = 0f;
    private PlayerInput playerInput;
    private InputAction movement;
    private InputAction look;
    

    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        movement = playerInput.actions["Move"];
        look = playerInput.actions["Look"];
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        var moveInput = movement.ReadValue<Vector2>();
        var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        var move = transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;
        transform.position += move;
    }

    private void Look()
    {
        var lookInput =  look.ReadValue<Vector2>();
        var mouseX = lookInput.x * lookSensitivity * Time.deltaTime;
        var mouseY = lookInput.y * lookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
