using System;
using System.Collections.Generic;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class Battler : MonoBehaviour
    {
        public abstract event Action<ChipCommandSO> BattlerAttacking;
        public abstract List<List<Vector2Int>> ValidRows { get; set; }
        public abstract Animator Animation { get; set; }
        public abstract void TakeDamage(int amount);
        public abstract bool IsAttacking { get; set; }
        public abstract ChipComponent ChipComponent { get; protected set; }
        public abstract void DealDamage();
        public virtual void Toggle(bool value)
        {
            Animation.enabled = value;
            Globals.ToggleScripts(gameObject, value);
        }
    }
}
