using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerShootComponent))]
[RequireComponent(typeof(ChipComponent))]
public class Player : Battler
{
    private const float _moveCooldown = 0.1f;
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    private PlayerShootComponent _playerShootComponent;
    private ChipSO _currentChip;

    public override event Action<ChipCommandSO> BattlerAttacking;
    public override List<List<Vector2Int>> ValidRows { get; set; }
    public override Animator Animation { get; set; }
    public override bool IsAttacking { get; set; }
    public override ChipComponent ChipComponent { get; protected set; }
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        Animation = GetComponentInChildren<Animator>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _playerShootComponent = GetComponent<PlayerShootComponent>();
        ChipComponent = GetComponent<ChipComponent>();

        _playerInput.PlayerMovement = _playerMovement;
        _playerInput.PlayerChipComponent = ChipComponent;
        _playerInput.PlayerShootComponent = _playerShootComponent;

        _playerMovement.Player = this;
        _playerInput.Player = this;
        ChipComponent.Battler = this;
    }

    private void Start()
    {
        ChipSelection.ChipsSelected += OnChipsSelected;
        ChipCommandSO.ChipExecuting += OnChipExecuting;
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

    public void StartMoveCooldown()
    {
        StartCoroutine(MoveCooldownRoutine());
    }

    public override void TakeDamage(int amount)
    {
        Animation.SetTrigger("TakeDamage");
    }

    private void OnChipsSelected(List<ChipSO> chips)
    {
        ChipComponent.Chips = chips;
        GetComponent<PlayerChipUI>().CreateChipImages(chips);
    }

    private void OnChipExecuting(Battler battler, ChipCommandSO chipCommand)
    {
        if (battler is not Player)
            return;

        _currentChip = ChipComponent.Chips[0];
        ChipComponent.Chips.RemoveAt(0);
    }

    public override void DealDamage()
    {
        BattlerAttacking?.Invoke(_currentChip.ChipCommandSO);
        Destroy(_currentChip);
    }

    private IEnumerator MoveCooldownRoutine()
    {
        yield return new WaitForSeconds(_moveCooldown);
        CanMove = true;
    }
}
