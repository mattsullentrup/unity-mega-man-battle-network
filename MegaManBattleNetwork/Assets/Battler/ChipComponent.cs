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
            // var ogChipCommand = Chips[0].ChipCommandSO;
            // var commandName = ogChipCommand.GetType().Name;
            // var so = ScriptableObject.CreateInstance(commandName);
            // var chipCommand = so as ChipCommandSO;
            if (chipCommand != null)
            {
                chipCommand.Battler = Battler;
                chipCommand.Execute();
            }

            // chip.ChipCommandSO.Battler = Battler;
            // chip.ChipCommandSO.Execute();
        }
    }
}
