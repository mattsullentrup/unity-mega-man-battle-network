using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;

public class ChipSelection : MonoBehaviour
{
    public static event Action<List<ChipSO>> ChipsSelected;

    [SerializeField] private GameObject _chipButtonPrefab;
    [SerializeField] private List<ChipSO> _chipsPool;
    [SerializeField] private GameObject _availableChipsContainer;
    [SerializeField] private GameObject _selectedChipsContainer;
    [SerializeField] private GameObject _roundProgressBar;
    [SerializeField] private GameObject _focusedChipTextureRect;
    [SerializeField] private Sprite _blankSprite;

    private Animation _animationPlayer;
    private const int _initialMaxChips = 5;
    private const int _maxSelectedChips = 3;
    private const int _maxChipContainerSize = 10;
    private int _maxAvailableChips = _initialMaxChips;
    private List<ChipSO> _selectedChips = new();
    private InputAction _secondaryAction;
    private Dictionary<GameObject, ChipSO> _buttonData = new();

    private void OnEnable()
    {
        BattleGrid.ChipSelectionStarting += OnChipSelectionStarting;
    }

    private void OnDisable()
    {
        BattleGrid.ChipSelectionStarting -= OnChipSelectionStarting;
    }

    private void Start()
    {
        // TODO: connect focus entered signal to all available chip buttons to display their image in the upper panel
        _secondaryAction = InputSystem.actions.FindAction("Secondary");
        _animationPlayer = GetComponent<Animation>();
        foreach (Transform child in _availableChipsContainer.transform)
        {
            _buttonData[child.gameObject] = null;
            child.gameObject.GetComponent<Button>().onClick.AddListener(SelectChip);
        }

        FillInBlankChips();
        _roundProgressBar.SetActive(false);
    }

    private void Update()
    {
        if (_secondaryAction.WasPressedThisFrame())
        {
            RemoveSelectedChip();
        }
    }

    public void OnOkButtonPressed()
    {
        // Detach selected chips from their associated buttons
        foreach (var button in new List<GameObject>(_buttonData.Keys))
        {
            foreach (var chip in _selectedChips)
            {
                if (_buttonData[button] == chip)
                {
                    _buttonData[button] = null;
                }
            }
        }

        // Reset selected chip container images
        foreach (Transform child in _selectedChipsContainer.transform)
        {
            Image image = child.gameObject.GetComponent<Image>();
            if (image != null && image.sprite != _blankSprite)
            {
                image.sprite = _blankSprite;
            }
        }

        ChipsSelected?.Invoke(new List<ChipSO>(_selectedChips));
        _selectedChips.Clear();
        EndChipSelection();
    }

    public void OnAddButtonPressed()
    {
        _maxAvailableChips += _selectedChipsContainer.transform.childCount;
        _maxAvailableChips = math.clamp(_maxSelectedChips, 0, _maxChipContainerSize);
        _selectedChips.Clear();
        var dummyChips = new List<ChipSO>();
        ChipsSelected?.Invoke(dummyChips);
        EndChipSelection();
    }

    private void RemoveSelectedChip()
    {
        var chipUI = _selectedChipsContainer.transform.GetChild(-1);
        chipUI.parent = null;
        foreach (Transform child in _availableChipsContainer.transform)
        {
            Image image = child.gameObject.GetComponent("Image") as Image;
            if (image != null && image.sprite == _blankSprite)
            {
                var index = child.GetSiblingIndex();
                chipUI.SetParent(_availableChipsContainer.transform);
                chipUI.SetSiblingIndex(index + 1);
                child.SetParent(null);
                chipUI.gameObject.GetComponent<Button>().onClick.AddListener(SelectChip);
                EventSystem.current.SetSelectedGameObject(chipUI.gameObject);
                return;
            }
        }
    }

    private void OnChipSelectionStarting()
    {
        _roundProgressBar.SetActive(false);
        FillInBlankChips();
    }

    private void FillInBlankChips()
    {
        var buttons = new List<GameObject>(_buttonData.Keys);
        for (int i = 0; i < _maxAvailableChips; i++)
        {
            var button = buttons[i];
            Image image = button.GetComponent("Image") as Image;
            if (image.sprite == _blankSprite)
            {
                int index = UnityEngine.Random.Range(0, _chipsPool.Count);
                var randomChip = Instantiate(_chipsPool[index]);
                _buttonData[button] = randomChip;
                image.sprite = randomChip.Sprite;
            }
        }

        EventSystem.current.SetSelectedGameObject(_availableChipsContainer.transform.GetChild(0).gameObject);
        _animationPlayer.Play("SlideIn");
    }

    private void SelectChip()
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject;
        if (_selectedChips.Count >= _maxSelectedChips)
            return;

        Image selectedImage = selectedButton.GetComponent("Image") as Image;
        if (selectedImage != null && selectedImage.sprite != _blankSprite)
        {
            foreach (Transform child in _selectedChipsContainer.transform)
            {
                Image image = child.gameObject.GetComponent("Image") as Image;
                if (image != null && image.sprite == _blankSprite)
                {
                    image.sprite = selectedImage.sprite;
                    _selectedChips.Add(_buttonData[selectedButton]);
                    selectedImage.sprite = _blankSprite;
                    return;
                }
            }
        }
    }

    private void EndChipSelection()
    {
        // _animationPlayer.PlayBackward("SlideChipSelectionContainer");
        _animationPlayer.Play("SlideOut");
        foreach (Transform child in _selectedChipsContainer.transform)
        {
            Destroy(child.gameObject);
            // probably need to remove entry from dictionary here
        }

        _roundProgressBar.SetActive(true);
    }
}