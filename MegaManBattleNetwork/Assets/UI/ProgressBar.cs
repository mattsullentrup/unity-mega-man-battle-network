using System.Collections.Generic;
using MegaManBattleNetwork;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private const int _start = 0;
    private const int _end = 760;
    private Image _containerImage;
    private RawImage _progressImage;
    private TextMeshProUGUI _startSelectionText;

    private void Awake()
    {
        _containerImage = GetComponent<Image>();
        _progressImage = GetComponentInChildren<RawImage>();
        _startSelectionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.RoundEnding += OnRoundEnding;
        ChipSelection.ChipsSelected += OnChipsSelected;

        _containerImage.gameObject.SetActive(false);
        _progressImage.gameObject.SetActive(false);
        _startSelectionText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.RoundEnding -= OnRoundEnding;
        ChipSelection.ChipsSelected -= OnChipsSelected;
    }

    private void Update()
    {
        _progressImage.rectTransform.sizeDelta = new Vector2
        (
            math.remap(0, 10, _start, _end, GameManager.Instance.ElapsedTime),
            _progressImage.rectTransform.sizeDelta.y
        );

        if
        (
            _startSelectionText.gameObject.activeSelf
            || Mathf.FloorToInt(_progressImage.rectTransform.sizeDelta.x) != _end
        )
            return;

        _startSelectionText.gameObject.SetActive(true);
    }

    private void OnChipsSelected(List<ChipSO> chips)
    {
        _containerImage.gameObject.SetActive(true);
        _progressImage.gameObject.SetActive(true);
        _startSelectionText.gameObject.SetActive(false);
    }

    private void OnRoundEnding()
    {
        _containerImage.gameObject.SetActive(false);
        _progressImage.gameObject.SetActive(false);
    }
}
