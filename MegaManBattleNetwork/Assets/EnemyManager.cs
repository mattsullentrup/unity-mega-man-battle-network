using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<List<Vector2Int>> _allRows;
    private List<List<Vector2Int>> _enemyRows;
    private List<Enemy> _enemies = new();

    public void Initialize(List<List<Vector2Int>> allRows, List<List<Vector2Int>> enemyRows)
    {
        _allRows = allRows;
        _enemyRows = enemyRows;
    }
}
