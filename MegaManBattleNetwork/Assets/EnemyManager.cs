using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<List<Vector2Int>> _allRows;
    private List<List<Vector2Int>> _enemyRows;
    private List<Enemy> _enemies;

    private void Start()
    {
        _enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID).ToList();
        return;
    }

    public void Initialize(List<List<Vector2Int>> allRows, List<List<Vector2Int>> enemyRows)
    {
        _allRows = allRows;
        _enemyRows = enemyRows;
    }
}
