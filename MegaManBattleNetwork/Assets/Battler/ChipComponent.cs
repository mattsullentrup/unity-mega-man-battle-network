using System.Collections.Generic;
using UnityEngine;

public class ChipComponent : MonoBehaviour
{
    public List<ChipSO> Chips
    {
        get => _chips;
        set => _chips = value;
    }
    public Battler Battler { get; set; }
    [SerializeField] private List<ChipSO> _chips = new();

    private void Start()
    {
        foreach (var chip in Chips)
        {
            chip.ChipCommandSO.Battler = Battler;
        }
    }

    public void ExecuteChip()
    {
        if (Chips.Count == 0)
            return;

        var chip = Chips[0];
        // chip.ChipCommandSO.Battler = Battler;
        chip.ChipCommandSO.Execute();
    }
}
