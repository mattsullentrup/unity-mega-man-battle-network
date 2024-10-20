using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerShootComponent))]
[RequireComponent(typeof(PlayerChipComponent))]
public class Player : Battler
{
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    private PlayerShootComponent _playerShootComponent;
    private PlayerChipComponent _playerChipComponent;
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _playerShootComponent = GetComponent<PlayerShootComponent>();
        _playerChipComponent = GetComponent<PlayerChipComponent>();

        _playerInput.PlayerMovement = _playerMovement;
        _playerInput.PlayerChipComponent = _playerChipComponent;
        _playerInput.PlayerShootComponent = _playerShootComponent;

        _playerMovement.Player = this;
    }
}
