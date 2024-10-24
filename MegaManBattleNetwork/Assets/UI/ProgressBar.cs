using System.Collections.Generic;
using MegaManBattleNetwork;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Image _containerImage;
    private RawImage _progressImage;

    private void Awake()
    {
        _containerImage = GetComponent<Image>();
        _progressImage = GetComponentInChildren<RawImage>();
    }

    private void Start()
    {
        GameManager.RoundEnding += OnRoundEnding;
        ChipSelection.ChipsSelected += OnChipsSelected;

        _containerImage.gameObject.SetActive(false);
        _progressImage.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.RoundEnding -= OnRoundEnding;
        ChipSelection.ChipsSelected -= OnChipsSelected;
    }

    private void OnChipsSelected(List<ChipSO> chips)
    {
        _containerImage.gameObject.SetActive(true);
        _progressImage.gameObject.SetActive(true);
    }

    private void OnRoundEnding()
    {
        _containerImage.gameObject.SetActive(false);
        _progressImage.gameObject.SetActive(false);
    }
}
