using UnityEngine;

namespace MegaManBattleNetwork
{
    [CreateAssetMenu(fileName = "CannonChipCommand", menuName = "Chips/CannonChipCommand")]
    public class CannonChipCommand : ChipCommandSO
    {
        public override void Execute()
        {
            base.Execute();
            Battler.Animation.SetTrigger("Attack");
        }
    }
}
