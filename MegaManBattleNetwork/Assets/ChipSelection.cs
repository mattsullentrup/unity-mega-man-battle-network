using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChipSelection : MonoBehaviour
{
    [SerializeField] private GameObject _chipButtonPrefab;
    [SerializeField] private List<ChipSO> _possibleChips;
    [SerializeField] private GameObject _chipContainer;
    [SerializeField] private GameObject _selectedChipsContainer;
    [SerializeField] private GameObject _roundProgressBar;
    [SerializeField] private GameObject _animationPlayer;
    [SerializeField] private GameObject _focusedChipTextureRect;

    private const int _initialMaxChips = 5;
    private const int _maxSelectedChips = 3;
    private const int _maxChipContainerSize = 10;
    private int _maxAvailableChips = _initialMaxChips;
    private List<ChipSO> _selectedChips = new();
    private List<GameObject> _dummyChips = new();
    private InputAction _secondaryAction;
    private Dictionary<GameObject, ChipSO> _buttonData = new();

    private void OnEnable()
    {
        Globals.ChipSelectionStarting += OnChipSelectionStarting;
    }

    private void OnDisable()
    {
        Globals.ChipSelectionStarting -= OnChipSelectionStarting;
    }

    private void Start()
    {
        _secondaryAction = InputSystem.actions.FindAction("Secondary");
        CreateDummyChips();
        CreateNewChips();
        _roundProgressBar.SetActive(false);
    }

    private void Update()
    {
        if (_secondaryAction.WasPressedThisFrame())
        {
            RemoveSelectedChip();
        }
    }

    private void RemoveSelectedChip()
    {
        var chipUI = _selectedChipsContainer.transform.GetChild(-1);
        chipUI.parent = null;
        foreach (Transform child in _chipContainer.transform)
        {
            Image image = child.gameObject.GetComponent("Image") as Image;
            if (image != null && image.sprite == null)
            {
                var index = child.GetSiblingIndex();
                chipUI.SetParent(_chipContainer.transform);
                chipUI.SetSiblingIndex(index + 1);
                child.SetParent(null);
                _dummyChips.Add(child.gameObject);
                chipUI.gameObject.GetComponent<Button>().onClick.AddListener(SelectChip);
                EventSystem.current.SetSelectedGameObject(chipUI.gameObject);
                return;
            }
        }
    }

    private void OnChipSelectionStarting()
    {
        _roundProgressBar.SetActive(false);
        CreateNewChips();
    }

    private void CreateNewChips()
    {
        var existingChips = new List<ChipSO>();
        foreach (Transform child in _chipContainer.transform)
        {
            Image image = child.gameObject.GetComponent("Image") as Image;
            if (image == null)
                continue;

            if (_buttonData.TryGetValue(child.gameObject, out ChipSO chip))
            {
                existingChips.Add(chip);
            }
        }

        var newChips = new List<ChipSO>();
        while (existingChips.Count + newChips.Count < _maxAvailableChips)
        {
            int index = Random.Range(0, _possibleChips.Count);
            var randomChip = _possibleChips[index];
            newChips.Add(randomChip);
        }

        SetupNewRound(newChips);
    }

    private void SetupNewRound(List<ChipSO> newChips)
    {
        foreach (Transform child in _chipContainer.transform)
        {
            Image image = child.gameObject.GetComponent("Image") as Image;
            if (image == null && newChips.Count > 0)
            {
                var chip = newChips[0];
                newChips.RemoveAt(0);

                var chipUI = CreateNewChipUI(chip);
                var index = child.GetSiblingIndex();
                chipUI.transform.SetParent(_chipContainer.transform);
                chipUI.transform.SetSiblingIndex(index + 1);
                child.SetParent(null);
                _dummyChips.Add(child.gameObject);
            }

            EventSystem.current.SetSelectedGameObject(_chipContainer.transform.GetChild(0).gameObject);
            // _animationPlayer.Play("SlideChipSelectionContainer");
            gameObject.SetActive(true);
        }
    }

    private GameObject CreateNewChipUI(ChipSO chip)
    {
        var button = Instantiate(_chipButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Image image = button.GetComponent("Image") as Image;
        _buttonData.Add(button, chip);
        button.GetComponent<Button>().onClick.AddListener(SelectChip);
        // chip_ui.focus_entered.connect(func(): _focused_chip_texture_rect.texture = chip.icon)
        return button;
    }

    private void CreateDummyChips()
    {
        while (_chipContainer.transform.childCount < _maxChipContainerSize)
        {
            var blankChip = Instantiate(_chipButtonPrefab, Vector3.zero, Quaternion.identity);
            blankChip.transform.SetParent(_chipContainer.transform);
        }
    }

    // private void SelectChip(GameObject chipUI)
    private void SelectChip()
    {
        var selectedChip = EventSystem.current.currentSelectedGameObject;
        if (_selectedChipsContainer.transform.childCount >= _maxSelectedChips)
            return;

        var dummyChip = _dummyChips[0];
        _dummyChips.RemoveAt(0);
        var index = selectedChip.transform.GetSiblingIndex();
    }

    private void EndChipSelection()
    {

    }

    private void OnOkButtonPressed()
    {

    }

    private void OnAddButtonPressed()
    {

    }
}
