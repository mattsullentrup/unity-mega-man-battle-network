using System.Collections.Generic;
using UnityEngine;

public class ChipComponent : MonoBehaviour
{
    public List<ChipSO> Chips;

    public void ExecuteChip()
    {
        if (Chips.Count == 0)
            return;

        var chip = Chips[0];
        chip.ChipCommandSO.Execute();
    }
}
