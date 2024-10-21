using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip")]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Texture _texture;
    [SerializeField] private String _name;
    [SerializeField] private ChipCommand _chipCommand;
    public ChipCommand ChipCommand => _chipCommand;

    public void Init(Texture texture = null, String name = "Chip", ChipCommand chipCommand = null)
    {
        _texture = texture;
        _name = name;
        _chipCommand = chipCommand;
    }
}
