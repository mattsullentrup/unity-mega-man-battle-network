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
        base.Execute();
        if (Battler is IBombAttacker bombAttacker)
        {
            bombAttacker.ThrowBomb();
        }
        // var bomb = Instantiate(_bombPrefab);
        // bomb.transform.position = Battler.transform.position + new Vector3(0.5f, 0, 0);
        // if (Battler is Enemy)
        // {
        //     bomb.transform.localScale = new(-1, 1, 1);
        // }
    }
}
