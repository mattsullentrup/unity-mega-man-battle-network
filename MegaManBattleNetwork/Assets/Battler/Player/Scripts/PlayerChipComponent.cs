using System.Collections.Generic;
using UnityEngine;

public class PlayerChipComponent : MonoBehaviour
{
    public Player Player { get; set; }
    public List<ChipSO> Chips;
    public ChipSO Chip;

    private void Start()
    {
        Chips.Add(Chip);
    }

    public void ExecuteChip()
    {
        if (Chips.Count == 0)
            return;

        var chip = Chips[0];
        chip.ChipCommandSO.Execute();
    }
}
