using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovement PlayerMovement { private get; set; }
    public PlayerShootComponent PlayerShootComponent { private get; set; }
    public PlayerChipComponent PlayerChipComponent { private get; set; }
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
        if (moveValue != Vector2.zero)
        {
            PlayerMovement.Move(moveValue);
        }

        if (_primaryAction.WasPressedThisFrame())
        {
            PlayerChipComponent.ExecuteChip();
        }

        if (_secondaryAction.WasPressedThisFrame())
        {
            PlayerShootComponent.Shoot();
        }
    }
}
