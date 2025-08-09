using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static event Action OnInteract;
    public static Vector2 Movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction, _interaction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _interaction = _playerInput.actions["Interact"];

        _interaction.performed += ctx => OnInteract?.Invoke();
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
    }
}
