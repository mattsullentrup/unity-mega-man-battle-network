using System;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "ChipCommand", menuName = "Chips/ChipCommand")]
public class ChipCommandSO : ScriptableObject, ICommand
{
    public int Damage;
    public float Delay;
    public List<Vector2Int> DamagableCells;
    public Battler Battler { get; set; }

    public virtual void Execute()
    {
        // BattlerAttacking?.Invoke(this);
    }
}
