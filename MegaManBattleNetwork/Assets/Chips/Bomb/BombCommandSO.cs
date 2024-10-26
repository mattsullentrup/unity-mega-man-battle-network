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
        if (Battler is not IBombAttacker bombAttacker)
            return;

        base.Execute();
        bombAttacker.ThrowBomb();
    }
}
