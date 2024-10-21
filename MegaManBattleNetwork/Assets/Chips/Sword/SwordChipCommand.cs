using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordChipCommand", menuName = "Chips/SwordChipCommand")]
public class SwordChipCommand : ChipCommandSO
{
    public override void Execute()
    {
        // Battler.Animation.ResetTrigger("Attack");
        Battler.Animation.SetTrigger("Attack");
        // Battler.Animation.Play("SwordAttack");
    }
}
