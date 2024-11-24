using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class Enemy : Battler
    {
        public static event Action<Enemy, ChipCommandSO> StartingAction;
        public override event Action<ChipCommandSO> BattlerAttacking;
        public override List<List<Vector2Int>> ValidRows { get; set; }
        public override Animator Animation { get; set; }

        [SerializeField] private float _actionCooldown = 4.0f;
        [SerializeField] protected ChipCommandSO _chipCommand;
        private Player _player;

        private void Awake()
        {
            Animation = GetComponentInChildren<Animator>();

            _chipCommand = Instantiate(_chipCommand);
            _chipCommand.Battler = this;

            for (int i = 0; i < _chipCommand.DamageableCells.Count; i++)
            {
                _chipCommand.DamageableCells[i] *= Vector2Int.left;
            }

            _chipCommand.ChipExecuting += OnChipExecuting;
        }

        public virtual void Start()
        {
            StartCoroutine(ActionRoutine());
            _player = FindFirstObjectByType<Player>();
        }

        private void OnDestroy()
        {
            _chipCommand.ChipExecuting -= OnChipExecuting;
        }

        public override void ExecuteChip()
        {
            _chipCommand.StartPosition = Globals.WorldToCell2D(transform.position);
            _chipCommand.Execute();
        }

        public override void TakeDamage(int amount)
        {
            if (_isInvulnerable)
                return;

            base.TakeDamage(amount);
            StopCoroutine(ActionRoutine());
        }

        public override void DealDamage()
        {
            if (_player == null)
                return;

            BattlerAttacking?.Invoke(_chipCommand);
            StartCoroutine(ActionRoutine());
        }

        protected IEnumerator ActionRoutine()
        {
            yield return new WaitForSeconds(_actionCooldown);
            _chipCommand.StartPosition = Globals.WorldToCell2D(transform.position);
            StartingAction?.Invoke(this, _chipCommand);
        }
    }
}
