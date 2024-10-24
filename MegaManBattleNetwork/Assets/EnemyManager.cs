using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    private List<List<Vector2Int>> _allRows;
    private List<List<Vector2Int>> _enemyRows;
    private List<Enemy> _enemies;
    public List<Enemy> Enemies => _enemies;
    public bool IsInitialized { get; private set; }

    private void Start()
    {
        Enemy.StartingAction += OnEnemyStartingAction;

        _enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID).ToList();
        foreach (var enemy in _enemies)
        {
            enemy.ValidRows = _enemyRows;
        }

        IsInitialized = true;
    }

    private void OnDestroy()
    {
        Enemy.StartingAction -= OnEnemyStartingAction;
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
