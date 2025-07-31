using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    public static bool Interaction;

    private PlayerInput _playerInput;
    private InputAction _moveAction, _interaction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _interaction = _playerInput.actions["Interact"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        Interaction = _interaction.triggered;
    }
}
