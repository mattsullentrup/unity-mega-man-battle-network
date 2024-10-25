using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

namespace MegaManBattleNetwork
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerShootComponent))]
    [RequireComponent(typeof(ChipComponent))]
    public class Player : Battler, IBombAttacker
    {
    [SerializeField] private GameObject _bombPrefab;
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private PlayerShootComponent _playerShootComponent;
        private ChipSO _currentChip;

        public override event Action<ChipCommandSO> BattlerAttacking;
        public override List<List<Vector2Int>> ValidRows { get; set; }
        public override Animator Animation { get; set; }
        public override ChipComponent ChipComponent { get; protected set; }
        // public bool CanMove { get; set; } = true;

        protected override void Awake()
        {
            Animation = GetComponentInChildren<Animator>();

            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _playerShootComponent = GetComponent<PlayerShootComponent>();
            ChipComponent = GetComponent<ChipComponent>();

            _playerInput.PlayerMovement = _playerMovement;
            _playerInput.PlayerChipComponent = ChipComponent;
            _playerInput.PlayerShootComponent = _playerShootComponent;

            ChipSelection.ChipsSelected += OnChipsSelected;
            ChipCommandSO.ChipExecuting += OnChipExecuting;

            base.Awake();
        }

        private void OnDestroy()
        {
            ChipSelection.ChipsSelected -= OnChipsSelected;
            ChipCommandSO.ChipExecuting -= OnChipExecuting;
        }

        public void ExecuteChip()
        {
            ChipComponent.ExecuteChip();
        }

        public override void TakeDamage(int amount)
        {
            if (_isInvulnerable)
                return;

            base.TakeDamage(amount);
            StartCoroutine(_playerMovement.TakeDamageRoutine());
        }

        public override void DealDamage()
        {
            BattlerAttacking?.Invoke(_currentChip.ChipCommandSO);
            Destroy(_currentChip);
        }

        private void OnChipsSelected(List<ChipSO> chips)
        {
            ChipComponent.Chips = chips;
            GetComponent<PlayerChipUI>().CreateChipImages(chips);
        }

        protected override void OnChipExecuting(Battler battler, ChipCommandSO chipCommand)
        {
            if (battler is not Player)
                return;

            base.OnChipExecuting(battler, chipCommand);
            _currentChip = ChipComponent.Chips[0];
            ChipComponent.Chips.RemoveAt(0);
        }

        public void ThrowBomb()
        {
            var bomb = Instantiate(_bombPrefab);
            bomb.transform.position = transform.position + new Vector3(0.5f, 0, 0);
        }
    }
}
