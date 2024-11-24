using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class ChipCommandSO : ScriptableObject, ICommand
    {
        public event Action<Battler, ChipCommandSO> ChipExecuting;
        public int Damage;
        public float Delay;
        public List<Vector2Int> DamageableCells;
        public Battler Battler { get; set; }
        public Vector2Int StartPosition { get; set; }

        public virtual void Execute()
        {
            ChipExecuting?.Invoke(Battler, this);
        }
    }
}
