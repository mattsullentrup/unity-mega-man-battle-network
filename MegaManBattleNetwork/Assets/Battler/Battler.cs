using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class Battler : MonoBehaviour, IDamagable
    {
        public float InvulnerableCooldown { get; private set; } = 3.0f;
        public float DamageTakenMoveCooldown { get; private set; } = 1.0f;
        public bool CanMove { get; set; } = true;
        public bool IsTakingDamage { get; set; }

        public abstract event Action<ChipCommandSO> BattlerAttacking;
        public abstract List<List<Vector2Int>> ValidRows { get; set; }
        public abstract Animator Animation { get; set; }
        public abstract void DealDamage();
        protected bool _isInvulnerable;

        protected virtual void Awake()
        {
            GameManager.RoundEnding += () => Toggle(false);
            ChipSelection.ChipsSelected += (List<ChipSO> chips) => Toggle(true);
            Toggle(false);
        }

        public abstract void ExecuteChip();


        public virtual void TakeDamage(int amount)
        {
            CanMove = false;
            Animation.SetTrigger("TakeDamage");
            GetComponent<HealthComponent>().DecreaseHealth(amount);
            StartCoroutine(TakeDamageRoutine());
            StartCoroutine(InvulnerableRoutine());
            GetComponent<SpriteFlash>().Flash();
        }

        public virtual void OnChipExecuting(Battler battler, ChipCommandSO chipCommand)
        {
            StartCoroutine(DamageDelayRoutine(chipCommand.Delay));
        }

        protected void Toggle(bool value)
        {
            Animation.enabled = value;
            foreach (var script in GetComponents<MonoBehaviour>())
            {
                script.enabled = value;
            }
        }

        protected IEnumerator DamageDelayRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            DealDamage();
        }

        private IEnumerator InvulnerableRoutine()
        {
            _isInvulnerable = true;
            yield return new WaitForSeconds(InvulnerableCooldown);
            _isInvulnerable = false;
        }

        private IEnumerator TakeDamageRoutine()
        {
            IsTakingDamage = true;
            yield return new WaitForSeconds(DamageTakenMoveCooldown);
            IsTakingDamage = false;
        }
    }
}
