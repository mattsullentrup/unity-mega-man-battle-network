using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private InputAction _moveAction;
    private InputAction _primaryAction;
    private InputAction _secondaryAction;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _primaryAction = InputSystem.actions.FindAction("Primary");
        _secondaryAction = InputSystem.actions.FindAction("Secondary");
    }

    private void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        playerMovement.Move(moveValue);
        if (_primaryAction.IsPressed())
        {

        }

        if (_secondaryAction.IsPressed())
        {

        }
    }
}
