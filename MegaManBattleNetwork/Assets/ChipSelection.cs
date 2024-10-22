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
    // private List<ChipSO> _possibleChips;
    private List<ChipSO> _selectedChips = new();
    private List<GameObject> _dummyChips = new();
    private InputAction _secondaryAction;

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

    }

    private void SetupNewRound(List<ChipSO> newChips)
    {

    }

    private GameObject CreateNewChipUI(ChipSO chip)
    {
        var button = Instantiate(_chipButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        return button;
    }

    private void CreateDummyChips()
    {

    }

    // private void SelectChip(GameObject chipUI)
    private void SelectChip()
    {

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
