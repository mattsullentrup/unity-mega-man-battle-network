using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Battler
{
    [SerializeField] private ChipCommandSO _currentChip;
    public event Action<Enemy> StartingAction;
    public override List<List<Vector2Int>> ValidRows { get; set; }
    public override Animator Animator { get; set; }

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
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
        yield return new WaitForSeconds(1);
        StartingAction?.Invoke(this);
    }
}
