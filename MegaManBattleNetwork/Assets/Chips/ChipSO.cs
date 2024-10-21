using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip", menuName = "Chips/Chip")]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Texture _texture;
    [SerializeField] private String _name;
    [SerializeField] private ChipCommandSO _chipCommandSO;
    public ChipCommandSO ChipCommandSO => _chipCommandSO;

    // public void Init(Texture texture = null, String name = "Chip", ChipCommand chipCommand = null)
    // {
    //     _texture = texture;
    //     _name = name;
    //     _chipCommand = chipCommand;
    // }
}
