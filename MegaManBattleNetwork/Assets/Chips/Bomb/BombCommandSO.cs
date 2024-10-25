using System.Collections;
using MegaManBattleNetwork;
using UnityEngine;

[CreateAssetMenu(fileName = "BombChipCommand", menuName = "Chips/BombChipCommand")]
public class BombCommandSO : ChipCommandSO
{
    [SerializeField] private AnimationClip _throwAnimation;

    private void Awake()
    {
        Delay = _throwAnimation.length;
    }

    public override void Execute()
    {
        // TODO: Refactor so the enemy can delay execution until it's bomb throw animation finishes.
        base.Execute();
        if (Battler is IBombAttacker bombAttacker)
        {
            bombAttacker.ThrowBomb();
        }
    }
}
