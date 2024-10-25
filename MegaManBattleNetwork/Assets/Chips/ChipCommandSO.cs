using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class ChipCommandSO : ScriptableObject, ICommand
    {
        // TODO: refactor this and maybe have battler implement chip command interface to define chip behaviour instead of the chip command
        // This might end up in more duplicate code but the current system makes it difficult to define separate behaviour for player and enemy when needed.
        public static event Action<Battler, ChipCommandSO> ChipExecuting;
        public int Damage;
        public float Delay;
        public List<Vector2Int> DamagableCells;
        public Battler Battler { get; set; }

        public virtual void Execute()
        {
            ChipExecuting?.Invoke(Battler, this);
        }
    }
}
