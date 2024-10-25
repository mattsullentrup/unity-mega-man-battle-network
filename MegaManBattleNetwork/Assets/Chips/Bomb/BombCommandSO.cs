using MegaManBattleNetwork;
using UnityEngine;

[CreateAssetMenu(fileName = "BombChipCommand", menuName = "Chips/BombChipCommand")]
public class BombCommandSO : ChipCommandSO
{
    [SerializeField] private GameObject _bombPrefab;

    public override void Execute()
    {
        base.Execute();
        // Battler.Animation.SetTrigger("Attack");
        var bomb = Instantiate(_bombPrefab);
    }
}
