using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action RoundEnding;
    [SerializeField] private int _roundLength = 10;

    private void OnEnable()
    {
        ChipSelection.ChipsSelected += OnChipsSelected;
    }

    private void OnDisable()
    {
        ChipSelection.ChipsSelected -= OnChipsSelected;
    }

    private void OnChipsSelected(List<ChipSO> chips)
    {
        StartCoroutine(RoundRoutine());
    }

    private IEnumerator RoundRoutine()
    {
        yield return new WaitForSeconds(_roundLength);
    }
}
