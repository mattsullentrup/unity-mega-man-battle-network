using System;
using System.Collections.Generic;
using UnityEngine;

namespace MegaManBattleNetwork
{
    [CreateAssetMenu(fileName = "SwordChipCommand", menuName = "Chips/SwordChipCommand")]
    public class SwordChipCommand : ChipCommandSO
    {
        public override void Execute()
        {
            base.Execute();
            Battler.Animation.SetTrigger("Attack");
        }
    }
}
