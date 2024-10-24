using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MegaManBattleNetwork
{
    public class PlayerInput : MonoBehaviour
    {
        public static event Action PlayerPressedSelect;
        public Player Player { private get; set; }
        public PlayerMovement PlayerMovement { private get; set; }
        public PlayerShootComponent PlayerShootComponent { private get; set; }
        public ChipComponent PlayerChipComponent { private get; set; }

        private readonly Vector2[] _directions = { Vector2.up, Vector2.right, Vector2.left, Vector2.down };
        private InputAction _moveAction;
        private InputAction _primaryAction;
        private InputAction _secondaryAction;
        private InputAction _selectAction;

        private void Start()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _primaryAction = InputSystem.actions.FindAction("Primary");
            _secondaryAction = InputSystem.actions.FindAction("Secondary");
            _selectAction = InputSystem.actions.FindAction("Select");
        }

        private void Update()
        {
            Vector2 moveValue = _moveAction.ReadValue<Vector2>();
            if (_directions.Contains(moveValue))
            {
                PlayerMovement.Move(Vector2Int.RoundToInt(moveValue));
            }

            if (_primaryAction.WasPressedThisFrame())
            {
                Player.ExecuteChip();
            }

            if (_secondaryAction.WasPressedThisFrame())
            {
                PlayerShootComponent.Shoot();
            }

            if (_selectAction.WasPressedThisFrame())
            {
                PlayerPressedSelect?.Invoke();
            }
        }
    }
}
