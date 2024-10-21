using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "ChipCommand", menuName = "Chips/ChipCommand")]
public class ChipCommandSO : ScriptableObject, ICommand
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    public List<Vector2Int> DamagableCells;
    public Battler Battler { get; set; }

    public virtual void Execute()
    {

    }
}
