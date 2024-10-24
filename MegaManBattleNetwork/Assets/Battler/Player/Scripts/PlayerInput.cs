using System;
using System.Collections;
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

        [SerializeField] private float _chipCooldown = 1.0f;
        [SerializeField] private float _moveCooldown = 0.1f;
        private readonly Vector2[] _directions = { Vector2.up, Vector2.right, Vector2.left, Vector2.down };
        private bool _justUsedChip;
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
            if (Player.CanMove && _directions.Contains(moveValue))
            {
                PlayerMovement.Move(Vector2Int.RoundToInt(moveValue));
                StartCoroutine(MoveCooldownRoutine());
            }

            if (_primaryAction.WasPressedThisFrame() && !_justUsedChip)
            {
                Player.ExecuteChip();
                StartCoroutine(ChipCooldownRoutine());
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

        public IEnumerator TakeDamageRoutine()
        {
            // Player.CanMove = false;
            StopCoroutine(MoveCooldownRoutine());
            yield return new WaitForSeconds(Player.DamageTakenMoveCooldown);
            Player.CanMove = true;
        }

        private IEnumerator ChipCooldownRoutine()
        {
            _justUsedChip = true;
            yield return new WaitForSeconds(_chipCooldown);
            _justUsedChip = false;
        }

        private IEnumerator MoveCooldownRoutine()
        {
            Player.CanMove = false;
            yield return new WaitForSeconds(_moveCooldown);
            Player.CanMove = true;
        }
    }
}
