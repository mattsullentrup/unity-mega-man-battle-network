using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class Battler : MonoBehaviour, IDamagable
    {
        public static event Action<Battler> BattlerDied;
        public float InvulnerableCooldown { get; private set; } = 3.0f;
        public float DamageTakenMoveCooldown { get; private set; } = 1.0f;
        public bool CanMove { get; set; } = true;
        public bool IsTakingDamage { get; set; }

        public abstract event Action<ChipCommandSO> BattlerAttacking;
        public abstract List<List<Vector2Int>> ValidRows { get; set; }
        public abstract Animator Animation { get; set; }
        public abstract void DealDamage();
        protected bool _isInvulnerable;

        // protected virtual void Awake()
        // {
        //     GameManager.RoundEnding += OnRoundEnding;
        //     ChipSelection.ChipsSelected += OnChipsSelected;
        //     // Toggle(false);
        // }

        // protected virtual void OnDestroy()
        // {
        //     GameManager.RoundEnding -= OnRoundEnding;
        //     ChipSelection.ChipsSelected -= OnChipsSelected;
        // }

        public abstract void ExecuteChip();

        public void Die()
        {
            Destroy(gameObject);
        }


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

        // private void OnRoundEnding()
        // {
        //     Toggle(false);
        // }
    }
}
