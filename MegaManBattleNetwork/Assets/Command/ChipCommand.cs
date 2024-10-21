using System.Collections.Generic;
using UnityEngine;

public abstract class ChipCommand : ICommand
{
    protected int _damage;
    protected float _delay;
    protected List<Vector2Int> _damagableCells;
    public abstract Battler Battler { get; set; }

    protected ChipCommand(int damage, float delay, List<Vector2Int> damagableCells)
    {
        _damage = damage;
        _delay = delay;
        _damagableCells = damagableCells;
    }

    public abstract void Execute();
}
