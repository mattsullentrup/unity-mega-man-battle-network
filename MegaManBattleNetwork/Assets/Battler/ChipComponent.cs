using System.Collections.Generic;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class ChipComponent : MonoBehaviour
    {
        public List<ChipSO> Chips
        {
            get => _chips;
            set => _chips = value;
        }
        public Battler Battler { get; set; }
        [SerializeField] private List<ChipSO> _chips = new();

        public void ExecuteChip()
        {
            if (Chips.Count == 0)
                return;

            var chip = Chips[0];
            chip.ChipCommandSO.Battler = Battler;

           chip.ChipCommandSO.Execute();
        }
    }
}
