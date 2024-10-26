using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MegaManBattleNetwork
{
    public class ChipComponent : MonoBehaviour
    {
        [field: SerializeField]
        public List<ChipSO> Chips { get; set; } = new();
        public Battler Battler { get; set; }

        private void Start()
        {
            Battler = GetComponent<Battler>();
        }

        public void ExecuteChip()
        {
            if (Chips.Count == 0)
                return;

            var chipCommand = Instantiate(Chips[0].ChipCommandSO);
            chipCommand.Battler = Battler;
            chipCommand.ChipExecuting += Battler.OnChipExecuting;
            chipCommand.Execute();
        }
    }
}
