using System;
using System.Collections.Generic;
using UnityEngine;

namespace MegaManBattleNetwork
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
    [RequireComponent(typeof(ChipComponent))]
    public class Player : Battler, IBombAttacker
    {
        [SerializeField] private GameObject _bombPrefab;
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private ChipCommandSO _currentChipCommand;

        public override event Action<ChipCommandSO> BattlerAttacking;
        public override List<List<Vector2Int>> ValidRows { get; set; }
        public override Animator Animation { get; set; }
        public ChipComponent ChipComponent { get; protected set; }

        private void Awake()
        {
            Animation = GetComponentInChildren<Animator>();

            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            ChipComponent = GetComponent<ChipComponent>();

            _playerInput.PlayerMovement = _playerMovement;
            _playerInput.PlayerChipComponent = ChipComponent;

            ChipSelection.ChipsSelected += OnChipsSelected;
            GameManager.RoundEnding += OnRoundEnding;
        }

        private void OnDestroy()
        {
            ChipSelection.ChipsSelected -= OnChipsSelected;
            GameManager.RoundEnding -= OnRoundEnding;
        }

        public override void ExecuteChip()
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
            BattlerAttacking?.Invoke(_currentChipCommand);
            Destroy(_currentChipCommand);
        }

        public override void OnChipExecuting(Battler battler, ChipCommandSO chipCommand)
        {
            if (battler is not Player)
                return;

            base.OnChipExecuting(battler, chipCommand);
            _currentChipCommand = chipCommand;
            ChipComponent.Chips.RemoveAt(0);
            GetComponent<PlayerChipUI>().DestroyChipImage();
        }

        public void ThrowBomb()
        {
            var bomb = Instantiate(_bombPrefab);
            bomb.transform.position = transform.position + new Vector3(0.75f, 0, 0);
        }

        private void ToggleInput(bool value)
        {
            var input = GetComponent<PlayerInput>();
            input.enabled = value;
        }

        private void OnChipsSelected(List<ChipSO> chips)
        {
            ToggleInput(true);
            ChipComponent.Chips = chips;
            GetComponent<PlayerChipUI>().CreateChipImages(chips);
        }

        private void OnRoundEnding()
        {
            ToggleInput(false);
        }
    }
}
