using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private List<List<Vector2Int>> _enemyRows;
        private List<Enemy> _enemies;
        private BattleGrid _battleGrid;
        public List<Enemy> Enemies => _enemies;
        public bool IsInitialized { get; private set; }

        private void Start()
        {
            _battleGrid = FindFirstObjectByType<BattleGrid>();
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

        public void Initialize(List<List<Vector2Int>> enemyRows)
        {
            _enemyRows = enemyRows;
        }

        public void ValidateNewPosition(Vector2Int direction, Enemy enemy)
        {
            // TODO: Change x or y position to zero when movement on that axis is invalid but not the other

            var newPos = Globals.WorldToCell2D(enemy.transform.position) + direction;
            if (!enemy.ValidRows.Any(list => list.Contains(newPos)))
                return;

            if (_enemies.Any(enemy => Globals.WorldToCell2D(enemy.transform.position) == newPos))
                return;

            var moveCommand = new MoveCommand(direction, enemy);
            moveCommand.Execute();
        }

        private void OnEnemyStartingAction(Enemy enemy, ChipCommandSO chipCommand)
        {
            if (_battleGrid.GetDamagableDefenders(chipCommand).Count != 0)
            {
                enemy.ExecuteChip();
            }
            else if (enemy is IMoveableEnemy)
            {
                var moveableEnemy = enemy as IMoveableEnemy;
                moveableEnemy?.Move(_player);
            }
        }
    }
}
