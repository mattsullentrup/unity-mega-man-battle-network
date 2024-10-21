using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Battler
{
    public event Action<Enemy, ChipCommandSO> StartingAction;
    public override event Action BattlerAttacking;

    public override List<List<Vector2Int>> ValidRows { get; set; }
    public override Animator Animator { get; set; }

    private ChipComponent _chipComponent;

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        _chipComponent = GetComponent<ChipComponent>();
        StartCoroutine(ActionRoutine());
    }

    public void ExecuteChip()
    {
        _chipComponent.ExecuteChip();
        BattlerAttacking?.Invoke();
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
        var chipCommand = _chipComponent.Chips[0].ChipCommandSO;
        chipCommand.Battler = this;
        StartingAction?.Invoke(this, _chipComponent.Chips[0].ChipCommandSO);
    }

    public override void TakeDamage()
    {
        throw new NotImplementedException();
    }
}
