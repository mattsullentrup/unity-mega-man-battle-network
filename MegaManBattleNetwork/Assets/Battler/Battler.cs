using System.Collections.Generic;
using UnityEngine;

public abstract class Battler : MonoBehaviour
{
    public abstract List<List<Vector2Int>> ValidRows { get; set; }
    public abstract Animator Animator { get; set; }
    public abstract void TakeDamage();
}
