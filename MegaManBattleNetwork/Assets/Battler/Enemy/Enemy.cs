using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class Enemy : Battler
    {
        public static event Action<Enemy, ChipCommandSO> StartingAction;
        public override event Action<ChipCommandSO> BattlerAttacking;
        public override List<List<Vector2Int>> ValidRows { get; set; }
        public override Animator Animation { get; set; }
        public override ChipComponent ChipComponent { get; protected set; }

        private void Awake()
        {
            Animation = GetComponentInChildren<Animator>();
            ChipComponent = GetComponent<ChipComponent>();
            ChipComponent.Battler = this;
            var chip = Instantiate(ChipComponent.Chips[0]);
            var chipCommand = Instantiate(ChipComponent.Chips[0].ChipCommandSO);
            chipCommand.Battler = this;
            for (int i = 0; i < chipCommand.DamagableCells.Count; i++)
            {
                chipCommand.DamagableCells[i] *= Vector2Int.left;
            }

            chip.ChipCommandSO = chipCommand;
            ChipComponent.Chips[0] = chip;
        }

        public void ExecuteChip()
        {
            ChipComponent.ExecuteChip();
            StartCoroutine(ActionRoutine());
        }

        public void Move(Player player)
        {
            var direction = new Vector2Int();
            if (player.transform.position.y > transform.position.y)
            {
                direction = Vector2Int.up;
            }
            else if (player.transform.position.y < transform.position.y)
            {
                direction = Vector2Int.down;
            }
            else
            {
                direction = Vector2Int.zero;
            }

            if (ValidRows.Any(list => list.Contains(Globals.WorldToCell2D(transform.position) + direction)))
            {
                var moveCommand = new MoveCommand(direction, this);
                moveCommand.Execute();
            }

            StartCoroutine(ActionRoutine());
        }

        private IEnumerator ActionRoutine()
        {
            yield return new WaitForSeconds(4.5f);
            StartingAction?.Invoke(this, ChipComponent.Chips[0].ChipCommandSO);
        }

        public override void TakeDamage(int amount)
        {
            if (_isInvulnerable)
                return;

            base.TakeDamage(amount);
            StopCoroutine(ActionRoutine());
            StartCoroutine(TakeDamageRoutine());
        }

        public override void DealDamage()
        {
            BattlerAttacking?.Invoke(ChipComponent.Chips[0].ChipCommandSO);
        }

        public override void Toggle(bool value)
        {
            base.Toggle(value);
            if (value == true)
            {
                StartCoroutine(ActionRoutine());
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator TakeDamageRoutine()
        {
            CanMove = false;
            yield return new WaitForSeconds(DamageTakenMoveCooldown);
            CanMove = true;
        }
    }
}
