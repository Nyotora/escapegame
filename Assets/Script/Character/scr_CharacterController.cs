using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSettings;

public class scr_CharacterController : MonoBehaviour
{
    private CharacterController characterController;

    private DefaultInput defaultInput;
    public Vector2 input_movement;
    public Vector2 input_view;

    private Vector3 newCameraRotation;
    private Vector3 newCaracterRotation;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;

    private void Awake()
    {
        defaultInput = new DefaultInput();

        defaultInput.Character.Movement.performed += e => input_movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_view = e.ReadValue<Vector2>();

        defaultInput.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCaracterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        CalculateView();
        CalculateMovement();
    }

    private void CalculateView()
    {
        
        newCaracterRotation.y += playerSettings.ViewXSensitivity * input_view.x * Time.deltaTime;
        transform.rotation = Quaternion.Euler(newCaracterRotation);
        
        newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? input_view.y : -input_view.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {
        var verticalSpeed = playerSettings.WalkingForwardSpeed * input_movement.y * Time.deltaTime;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed * input_movement.x * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, -5, verticalSpeed);

        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        characterController.Move(newMovementSpeed);
    }

    public void enableInput()
    {
        defaultInput.Enable();
    }

    public void disableInput()
    {
        defaultInput.Disable();
        input_movement = new Vector2(0, 0);
        input_view = new Vector2(0, 0);
    }
}
