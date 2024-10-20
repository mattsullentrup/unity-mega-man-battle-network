using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector2 _direction;
    private Battler _battler;

    public MoveCommand(Vector2 direction, Battler battler)
    {
        _direction = direction;
        _battler = battler;
    }

    public void Execute()
    {
        if (_battler == null) return;

        Vector2 battlerPos = _battler.transform.position;
        var newPosition = battlerPos + _direction;
        _battler.transform.position = newPosition;
    }
}
