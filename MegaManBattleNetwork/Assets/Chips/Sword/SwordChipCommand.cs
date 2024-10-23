using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordChipCommand", menuName = "Chips/SwordChipCommand")]
public class SwordChipCommand : ChipCommandSO
{
    // public override event Action<Battler, ChipCommandSO> ChipExecuting;

    // public override static event Action<Battler, ChipCommandSO> ChipExecuting;

    public override void Execute()
    {
        base.Execute();
        Battler.Animation.SetTrigger("Attack");
    }
}
