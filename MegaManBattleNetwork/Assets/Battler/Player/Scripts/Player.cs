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
    public bool CanMove { get; set; } = true;
    public override List<List<Vector2Int>> ValidRows { get; set; }
    public override Animator Animator { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _playerShootComponent = GetComponent<PlayerShootComponent>();
        _playerChipComponent = GetComponent<ChipComponent>();

        _playerInput.PlayerMovement = _playerMovement;
        _playerInput.PlayerChipComponent = _playerChipComponent;
        _playerInput.PlayerShootComponent = _playerShootComponent;

        _playerMovement.Player = this;
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

    public override void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
