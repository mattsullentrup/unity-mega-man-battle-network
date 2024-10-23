using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip", menuName = "Chips/Chip")]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private ChipCommandSO _chipCommandSO;
    public ChipCommandSO ChipCommandSO => _chipCommandSO;
    public Sprite Sprite => _sprite;

    private void Awake()
    {
        Instantiate(_chipCommandSO);
        // _chipCommandSO = CreateInstance<ChipCommandSO>();
    }
}
