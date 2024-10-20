using System.Collections.Generic;
using UnityEngine;

public abstract class Battler : MonoBehaviour
{
    public abstract List<List<Vector2Int>> ValidRows { get; set; }
}
