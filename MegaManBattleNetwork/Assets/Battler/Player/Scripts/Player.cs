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
    private ChipComponent _playerChipComponent;

    public override event Action<ChipCommandSO> BattlerAttacking;
    public override List<List<Vector2Int>> ValidRows { get; set; }
    public override Animator Animation { get; set; }
    public override bool IsAttacking { get; set; }
    public ChipComponent ChipComponent { get; }
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        Animation = GetComponentInChildren<Animator>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _playerShootComponent = GetComponent<PlayerShootComponent>();
        _playerChipComponent = GetComponent<ChipComponent>();

        _playerInput.PlayerMovement = _playerMovement;
        _playerInput.PlayerChipComponent = _playerChipComponent;
        _playerInput.PlayerShootComponent = _playerShootComponent;

        _playerMovement.Player = this;
        _playerInput.Player = this;
        _playerChipComponent.Battler = this;
    }

    public void ExecuteChip()
    {
        BattlerAttacking?.Invoke(_playerChipComponent.Chips[0].ChipCommandSO);
        _playerChipComponent.ExecuteChip();
    }

    public void StartMoveCooldown()
    {
        StartCoroutine(MoveCooldownRoutine());
    }

    private IEnumerator MoveCooldownRoutine()
    {
        yield return new WaitForSeconds(_moveCooldown);
        CanMove = true;
    }

    public override void TakeDamage(int amount)
    {
        Animation.SetTrigger("TakeDamage");
    }
}
