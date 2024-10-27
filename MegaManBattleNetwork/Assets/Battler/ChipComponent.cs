using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MegaManBattleNetwork
{
    public class ChipComponent : MonoBehaviour
    {
        [field: SerializeField]
        public List<ChipSO> Chips { get; set; } = new();
        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        public void ExecuteChip()
        {
            if (Chips.Count == 0)
                return;

            var chipCommand = Instantiate(Chips[0].ChipCommandSO);
            chipCommand.Battler = _player;
            chipCommand.ChipExecuting += _player.OnChipExecuting;
            chipCommand.StartPosition = Globals.WorldToCell2D(_player.transform.position);
            chipCommand.Execute();
        }
    }
}
