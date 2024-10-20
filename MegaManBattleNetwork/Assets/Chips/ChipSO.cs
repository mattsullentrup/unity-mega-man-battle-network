using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip")]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Texture _texture;
    [SerializeField] private String _name;
    [SerializeField] private ChipCommand _chipCommand;
}
