using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSettings;

public class scr_CharacterController : MonoBehaviour
{
    private CharacterController characterController;

    private bool enable = true;

    private DefaultInput defaultInput;
    public Vector2 input_movement;
    public Vector2 input_view;

    private Vector3 newCameraRotation;
    private Vector3 newCaracterRotation;

    private bool canCalculate;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;

    private void Awake()
    {
        canCalculate = true;

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
        if (canCalculate)
        {
            CalculateView();
        }
        else
        {
            CalculateXView();
        }
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

    private void CalculateXView()
    {
        newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? input_view.y : -input_view.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {
        var verticalSpeed = playerSettings.WalkingForwardSpeed * input_movement.y * Time.deltaTime;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed * input_movement.x * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, -2, verticalSpeed);

        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        characterController.Move(newMovementSpeed);
    }

    public void enableInput()
    {
        defaultInput.Enable();
        enable = true;
    }

    public void disableInput()
    {
        defaultInput.Disable();
        input_movement = new Vector2(0, 0);
        input_view = new Vector2(0, 0);
        enable = false;
    }

    public void disableCalculating()
    {
        canCalculate = false;
    }

    public void enableCalculating()
    {
        canCalculate = true;
    }

    public bool ePressed()
    {
        if (defaultInput.Character.Interaction.WasPressedThisFrame())
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool escPressed()
    {
        if (defaultInput.Character.Pause.WasPressedThisFrame())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool canMove()
    {
        return enable;
    }
}
