using System.Collections.Generic;
using UnityEngine;

public class PlayerChipComponent : MonoBehaviour
{
    public Player Player { get; set; }
    public List<ChipSO> Chips;

    // private void Start()
    // {
    //     var swordChip = ScriptableObject.CreateInstance<ChipSO>();
    //     swordChip.Init(null, "Chip", new SwordChipCommand());
    //     Chips.Add(swordChip);
    // }

    public void ExecuteChip()
    {
        if (Chips.Count == 0)
            return;

        var chip = Chips[0];
        chip.ChipCommandSO.Execute();
    }
}
