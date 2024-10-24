using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public abstract class Battler : MonoBehaviour, IDamagable
    {
        public abstract event Action<ChipCommandSO> BattlerAttacking;
        public abstract List<List<Vector2Int>> ValidRows { get; set; }
        public abstract Animator Animation { get; set; }
        public abstract bool IsAttacking { get; set; }
        public float DamageTakenCooldown { get; private set; } = 2.0f;
        public float InvulnerableCooldown { get; private set; } = 4.0f;
        public abstract ChipComponent ChipComponent { get; protected set; }
        public virtual void TakeDamage(int amount)
        {
            GetComponent<SpriteFlash>().Flash();
        }
        public abstract void DealDamage();

        public virtual void Toggle(bool value)
        {
            Animation.enabled = value;
            Globals.ToggleScripts(gameObject, value);
        }
    }
}
