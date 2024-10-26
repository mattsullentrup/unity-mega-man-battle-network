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
        public List<Vector2Int> DamagableCells;
        public Battler Battler { get; set; }

        public virtual void Execute()
        {
            if (Battler is Enemy)
            {
                for (int i = 0; i < DamagableCells.Count; i++)
                {
                    DamagableCells[i] *= Vector2Int.left;
                }
            }

            ChipExecuting?.Invoke(Battler, this);
        }
    }
}
