using System.Collections.Generic;
using UnityEngine;

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

            var chip = Chips[0];
            chip.ChipCommandSO.Battler = Battler;

           chip.ChipCommandSO.Execute();
        }
    }
}
