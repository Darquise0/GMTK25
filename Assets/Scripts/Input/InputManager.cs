using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction interactAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];

        interactAction.performed += ctx => GameEventsManager.instance.inputEvents.SubmitPressed();
    }

    private void Update()
    {
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        GameEventsManager.instance.inputEvents.MovePressed(moveDir);
    }
}
