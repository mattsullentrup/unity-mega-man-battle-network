using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordChipCommand", menuName = "Chips/SwordChipCommand")]
public class SwordChipCommand : ChipCommandSO
{
    public override void Execute()
    {
        Debug.Log("executing sword chip command");
    }
}
