using System.Collections.Generic;
using UnityEngine;

public class SwordChipCommand : ChipCommand
{
    public override Battler Battler { get; set; }

    public SwordChipCommand(int damage, float delay, List<Vector2Int> damagableCells)
        : base(damage, delay, damagableCells) {}

    public override void Execute()
    {
        Debug.Log("executing chip command");
    }
}
