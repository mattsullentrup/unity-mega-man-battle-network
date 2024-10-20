using UnityEngine;

public abstract class ChipCommand : ICommand
{
    public abstract Battler Battler { get; set; }

    public abstract void Execute();
}
