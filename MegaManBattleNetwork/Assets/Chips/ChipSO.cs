using System;
using UnityEngine;

namespace MegaManBattleNetwork
{
    [CreateAssetMenu(fileName = "Chip", menuName = "Chips/Chip")]
    public class ChipSO : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;

        [field: SerializeField]
        public ChipCommandSO ChipCommandSO { get; set; }
        public Sprite Sprite => _sprite;
        public string ChipName => _name;
    }
}
