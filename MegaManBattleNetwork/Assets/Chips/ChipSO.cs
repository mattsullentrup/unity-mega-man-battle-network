using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip", menuName = "Chips/Chip")]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Texture _texture;
    [SerializeField] private string _name;
    [SerializeField] private ChipCommandSO _chipCommandSO;
    public ChipCommandSO ChipCommandSO => _chipCommandSO;
}
