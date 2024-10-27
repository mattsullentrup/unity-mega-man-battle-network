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
        public PlayerMovement PlayerMovement { private get; set; }
        public ChipComponent PlayerChipComponent { private get; set; }

        [SerializeField] private float _chipCooldown = 1.0f;
        private readonly Vector2[] _directions = { Vector2.up, Vector2.right, Vector2.left, Vector2.down };
        private bool _justUsedChip;
        private InputAction _moveAction;
        private InputAction _primaryAction;
        private InputAction _selectAction;
        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
            _moveAction = InputSystem.actions.FindAction("Move");
            _primaryAction = InputSystem.actions.FindAction("Primary");
            _selectAction = InputSystem.actions.FindAction("Secondary");
        }

        private void Update()
        {
            if (GameManager.Instance.IsSelectingChips)
                return;

            Vector2 moveValue = _moveAction.ReadValue<Vector2>();
            if (_directions.Contains(moveValue))
            {
                PlayerMovement.Move(Vector2Int.RoundToInt(moveValue));
            }

            if (_primaryAction.WasPressedThisFrame() && !_justUsedChip)
            {
                _player.ExecuteChip();
                StartCoroutine(ChipCooldownRoutine());
            }

            if (_selectAction.WasPressedThisFrame())
            {
                PlayerPressedSelect?.Invoke();
            }
        }

        private IEnumerator ChipCooldownRoutine()
        {
            _justUsedChip = true;
            yield return new WaitForSeconds(_chipCooldown);
            _justUsedChip = false;
        }
    }
}
