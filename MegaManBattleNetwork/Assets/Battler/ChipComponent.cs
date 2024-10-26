using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MegaManBattleNetwork
{
    public class ChipComponent : MonoBehaviour
    {
        [field: SerializeField]
        public List<ChipSO> Chips { get; set; } = new();
        // public Battler Battler { get; set; }
        private Player _player;

        private void Start()
        {
            // Battler = GetComponent<Battler>();
            _player = GetComponent<Player>();
        }

        public void ExecuteChip()
        {
            if (Chips.Count == 0)
                return;

            var chipCommand = Instantiate(Chips[0].ChipCommandSO);
            chipCommand.Battler = _player;
            chipCommand.ChipExecuting += _player.OnChipExecuting;
            chipCommand.Execute();
        }
    }
}
