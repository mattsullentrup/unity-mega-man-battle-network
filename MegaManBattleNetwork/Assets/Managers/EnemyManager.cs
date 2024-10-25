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

        private void OnEnemyStartingAction(Enemy enemy, ChipCommandSO chipCommand)
        {
            if (_battleGrid.GetDamagableDefenders(chipCommand).Count != 0)
            {
                enemy.ExecuteChip();
            }
            else
            {
                enemy.Move(_player);
            }
        }
    }
}
