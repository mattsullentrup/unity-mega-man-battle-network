using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    private List<List<Vector2Int>> _allRows;
    private List<List<Vector2Int>> _enemyRows;
    private List<Enemy> _enemies;
    public List<Enemy> Enemies { get; }

    private void Start()
    {
        _enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID).ToList();
        foreach (var enemy in _enemies)
        {
            enemy.StartingAction += OnEnemyStartingAction;
            enemy.ValidRows = _enemyRows;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.StartingAction -= OnEnemyStartingAction;
        }
    }

    public void Initialize(List<List<Vector2Int>> allRows, List<List<Vector2Int>> enemyRows)
    {
        _allRows = allRows;
        _enemyRows = enemyRows;
    }

    private void OnEnemyStartingAction(Enemy enemy, ChipCommandSO chipCommand)
    {
        if (BattleGrid.Instance.GetDamagableDefenders(chipCommand).Count != 0)
        {
            enemy.ExecuteChip();
        }
        else
        {
            enemy.Move(_player);
        }
    }
}
