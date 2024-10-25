using System.Collections;
using MegaManBattleNetwork;
using UnityEngine;

[CreateAssetMenu(fileName = "BombChipCommand", menuName = "Chips/BombChipCommand")]
public class BombCommandSO : ChipCommandSO
{
    [SerializeField] private GameObject _bombPrefab;

    private void Awake()
    {
        Debug.Log("bomb awake");
    }

    public override void Execute()
    {
        base.Execute();
        var bomb = Instantiate(_bombPrefab);
        bomb.transform.position = Battler.transform.position + new Vector3(0.5f, 0, 0);
        if (Battler is Enemy)
        {
            bomb.transform.localScale = new(-1, 1, 1);
        }
    }
}
