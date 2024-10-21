using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleGrid : MonoBehaviour
{
    private enum Row
    {
        Top,
        Middle,
        Bottom
    }

    [SerializeField] private Player _player;
    [SerializeField] private EnemyManager _enemyManager;

    private readonly Vector2Int _playerStartPos = new(-2, 0);
    private readonly Vector2Int[,] _cells =
    {
        { new(-3, 1), new(-2, 1), new(-1, 1) },
        { new(-3, 0), new(-2, 0), new(-1, 0) },
        { new(-3, -1), new(-2, -1), new(-1, -1) }
    };
    private List<List<Vector2Int>> _enemyRows = new();
    private List<List<Vector2Int>> _playerRows = new();
    private List<List<Vector2Int>> _allRows = new();

    private Grid _grid;
    public Grid Grid => _grid;
    private static BattleGrid _instance;
    public static BattleGrid Instance => _instance;

    private void Awake()
    {
        _instance = this;
        _grid = GetComponent<Grid>();

        SetupRows();
        _enemyManager.Initialize(_allRows, _enemyRows);

        _player.transform.position = new Vector3(_playerStartPos.x, _playerStartPos.y, 0);
        _player.ValidRows = _playerRows;
    }

    public List<Battler> GetDamagableDefenders(ChipCommandSO chipCommand)
    {
        var damagableCells = chipCommand.DamagableCells;
        var attacker = chipCommand.Battler;
        List<Battler> defenders;
        if (attacker is Player)
        {
            defenders = (List<Battler>)_enemyManager.Enemies.Cast<Battler>().OrderBy(x => x).Select(x => x);
        }
        else
        {
            defenders = new()
            {
                _player
            };
        }

        var globalDamagableCells = new List<Vector2Int>();
        foreach (var cell in chipCommand.DamagableCells)
        {
            globalDamagableCells.Add(cell + Globals.WorldToCell2D(attacker.transform.position));
        }

        var damagableDefenders = new List<Battler>();
        foreach (var row in _allRows)
        {
            foreach (var cell in globalDamagableCells)
            {
                if (!row.Contains(cell))
                    continue;
                
                foreach (var defender in defenders)
                {
                    if (Globals.WorldToCell2D(defender.transform.position) == cell)
                    {
                        damagableDefenders.Add(defender);
                    }
                }
            }
        }

        return damagableDefenders;
    }

    private void SetupRows()
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            List<Vector2Int> playerRow = new();
            List<Vector2Int> enemyRow = new();
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                var newCell = _cells[i, j] + new Vector2Int(3, 0);
                enemyRow.Add(newCell);
                playerRow.Add(_cells[i, j]);
            }

            _playerRows.Add(playerRow);
            _enemyRows.Add(enemyRow);

            var wholeRow = playerRow.Concat(enemyRow);
            _allRows.Add(wholeRow.ToList());
        }
    }
}
