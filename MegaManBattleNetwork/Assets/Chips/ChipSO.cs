using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip", menuName = "Chips/Chip")]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;

    [field: SerializeField]
    public ChipCommandSO ChipCommandSO { get; set; }
    public Sprite Sprite => _sprite;

    private void OnEnable()
    {
        Instantiate(ChipCommandSO);
    }
}
