using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipCommand", menuName = "Chips/ChipCommand")]
public class ChipCommandSO : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private List<Vector2Int> _damagableCells;
    [SerializeField] private ChipCommand _chipCommand;

    public void Execute()
    {
        // ChipCommand.Execute();
    }
}
