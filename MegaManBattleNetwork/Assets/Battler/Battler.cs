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
        public abstract ChipComponent ChipComponent { get; protected set; }
        public abstract void DealDamage();
        protected bool _isInvulnerable;

        public virtual void Toggle(bool value)
        {
            Animation.enabled = value;
            Globals.ToggleScripts(gameObject, value);
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
