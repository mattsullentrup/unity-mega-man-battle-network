using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChipCommandSO : ScriptableObject, ICommand
{
    // TODO: flip DamagableCells sign depending on battler type.
    public static event Action<Battler, ChipCommandSO> ChipExecuting;
    public int Damage;
    public float Delay;
    public List<Vector2Int> DamagableCells;
    public Battler Battler { get; set; }

    public virtual void Execute()
    {
        ChipExecuting?.Invoke(Battler, this);
    }
}
