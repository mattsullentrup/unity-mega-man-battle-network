using System;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    // public static event Action<int> PlayerTookDamage;
    // public static event Action ChipSelectionStarting;
    // public static event Action<List<ChipSO>> ChipsSelected;
    // public static event Action CanStartChipSelection;
    public const int CellSize = 32;
    public static Vector2Int WorldToCell2D(Vector3 cell)
    {
        Vector2 pos = new(cell.x, cell.y);
        var newPos = Vector2Int.RoundToInt(pos);
        return newPos;
    }

    public static void ToggleScripts(GameObject entity, bool value)
    {
        var scripts = entity.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = value;
        }
    }
}
