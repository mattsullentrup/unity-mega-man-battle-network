using UnityEngine;

public static class Globals
{
    public const int CellSize = 32;
    public static Vector2Int WorldToCell2D(Vector3 cell)
    {
        Vector2 pos = new(cell.x, cell.y);
        var newPos = Vector2Int.RoundToInt(pos);
        return newPos;
    }
}
