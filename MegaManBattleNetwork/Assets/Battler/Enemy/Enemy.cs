using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class Enemy : Battler
    {
        public static event Action<Enemy, ChipCommandSO> StartingAction;
        public override event Action<ChipCommandSO> BattlerAttacking;
        public override List<List<Vector2Int>> ValidRows { get; set; }
        public override Animator Animation { get; set; }
        public override ChipComponent ChipComponent { get; protected set; }

        [SerializeField] private float _actionCooldown = 4.0f;

        protected override void Awake()
        {
            Animation = GetComponentInChildren<Animator>();
            ChipComponent = GetComponent<ChipComponent>();
            var chip = Instantiate(ChipComponent.Chips[0]);
            var chipCommand = Instantiate(ChipComponent.Chips[0].ChipCommandSO);
            chipCommand.Battler = this;

            for (int i = 0; i < chipCommand.DamagableCells.Count; i++)
            {
                chipCommand.DamagableCells[i] *= Vector2Int.left;
            }

            chip.ChipCommandSO = chipCommand;
            ChipComponent.Chips[0] = chip;

            base.Awake();
        }

        private void Start()
        {
            ChipCommandSO.ChipExecuting += OnChipExecuting;
            StartCoroutine(ActionRoutine());
        }

        private void OnDestroy()
        {
            ChipCommandSO.ChipExecuting -= OnChipExecuting;
        }

        public void ExecuteChip()
        {
            ChipComponent.ExecuteChip();
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
            StartCoroutine(ActionRoutine());
            BattlerAttacking?.Invoke(ChipComponent.Chips[0].ChipCommandSO);
        }

        protected void ValidateNewPosition(Vector2Int direction)
        {
            if (ValidRows.Any(list => list.Contains(Globals.WorldToCell2D(transform.position) + direction)))
            {
                var moveCommand = new MoveCommand(direction, this);
                moveCommand.Execute();
            }
        }

        protected IEnumerator ActionRoutine()
        {
            yield return new WaitForSeconds(_actionCooldown);
            StartingAction?.Invoke(this, ChipComponent.Chips[0].ChipCommandSO);
        }

        protected override void OnChipExecuting(Battler battler, ChipCommandSO chipCommand)
        {
            if (battler is not Enemy)
                return;

            base.OnChipExecuting(battler, chipCommand);
        }
    }
}
