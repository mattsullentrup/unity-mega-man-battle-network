using UnityEngine;

public class SwordChipCommand : ChipCommand
{
    public override Battler Battler { get; set; }

    public override void Execute()
    {
        Debug.Log("executing chip command");
    }
}
