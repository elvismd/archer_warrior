using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField]
    private PlayerInput PlayerInput;

    public Vector2 Move { get; private set; }
    public bool Shoot
    {
        get { return _shootAction.WasPressedThisFrame(); }
    }

    public bool Pause
    {
        get { return _pauseAction.WasPressedThisFrame(); }
    }

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _pauseAction;

    private void Awake()
    {
        Instance = this;

        //HideCursor();

        _currentMap = PlayerInput.currentActionMap;
        _moveAction = _currentMap.FindAction("Move");
        _shootAction = _currentMap.FindAction("Shoot");
        _pauseAction = _currentMap.FindAction("Pause");

        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Move = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _currentMap.Enable();
    }

    private void OnDisable()
    {
        _currentMap.Disable();
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
