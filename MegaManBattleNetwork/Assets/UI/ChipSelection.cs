using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;
using System.Linq;
using TMPro;

namespace MegaManBattleNetwork
{
    public class ChipSelection : MonoBehaviour
    {
        public static event Action<List<ChipSO>> ChipsSelected;

        [SerializeField] private GameObject _chipButtonPrefab;
        [SerializeField] private List<ChipSO> _chipsPool;
        [SerializeField] private GameObject _availableChipsContainer;
        [SerializeField] private GameObject _selectedChipsContainer;
        [SerializeField] private GameObject _focusedChipImage;
        [SerializeField] private Sprite _blankSprite;
        [SerializeField] private TextMeshProUGUI _highlightedChipText;

        private Animator _animationPlayer;
        private const int _initialMaxChips = 5;
        private const int _maxSelectedChips = 3;
        private const int _maxChipContainerSize = 10;
        private int _maxAvailableChips = _initialMaxChips;
        private List<ChipSO> _selectedChips = new();
        private InputAction _primaryAction;
        private InputAction _secondaryAction;
        private Dictionary<GameObject, ChipSO> _buttonData = new();

        private void OnEnable()
        {
            GameManager.RoundEnding += OnRoundEnding;
        }

        private void OnDisable()
        {
            GameManager.RoundEnding -= OnRoundEnding;
        }

        private void Start()
        {
            _primaryAction = InputSystem.actions.FindAction("Primary");
            _secondaryAction = InputSystem.actions.FindAction("Secondary");
            _animationPlayer = GetComponent<Animator>();
            foreach (Transform child in _availableChipsContainer.transform)
            {
                _buttonData[child.gameObject] = null;
                child.gameObject.GetComponent<Button>().onClick.AddListener(SelectChip);
            }

            FillInBlankChips();
        }

        private void Update()
        {
            if (_primaryAction.WasPressedThisFrame())
            {
                if (EventSystem.current.currentSelectedGameObject.name == "OkButton")
                {
                    OnOkButtonPressed();
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "AddButton")
                {
                    OnAddButtonPressed();
                }
                else
                {
                    SelectChip();
                }
            }
            else if (_secondaryAction.WasPressedThisFrame())
            {
                RemoveSelectedChip();
            }

            SetFocusedChipImage();
        }

        private void SetFocusedChipImage()
        {
            if (EventSystem.current == null)
                return;

            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected == null)
                return;

            var image = _focusedChipImage.GetComponent<Image>().sprite;
            if (selected.GetComponent<Image>().sprite == image)
                return;

            if (_buttonData.ContainsKey(selected) && _buttonData[selected] != null)
            {
                _focusedChipImage.GetComponent<Image>().sprite = _buttonData[selected].Sprite;
                _highlightedChipText.text = _buttonData[selected].ChipName;
            }
        }

        private void OnRoundEnding()
        {
            // EventSystem.current.enabled = true;
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
                    // var chipCommand = Instantiate(randomChip.ChipCommandSO);
                    // randomChip.ChipCommandSO = chipCommand;
                    _buttonData[button] = randomChip;
                    image.sprite = randomChip.Sprite;
                }
            }

            EventSystem.current.SetSelectedGameObject(_availableChipsContainer.transform.GetChild(0).gameObject);
            _animationPlayer.SetTrigger("SlideIn");
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

        private void RemoveSelectedChip()
        {
            for (int i = _selectedChipsContainer.transform.childCount - 1; i >= 0; i--)
            {
                var child = _selectedChipsContainer.transform.GetChild(i);
                if (child.gameObject.GetComponent<Image>().sprite != _blankSprite)
                {
                    child.gameObject.GetComponent<Image>().sprite = _blankSprite;
                    var selectedChip = _selectedChips[child.GetSiblingIndex()];
                    _selectedChips.Remove(selectedChip);

                    var button = _buttonData.FirstOrDefault(x => x.Value == selectedChip).Key;
                    button.GetComponent<Image>().sprite = selectedChip.Sprite;
                    EventSystem.current.SetSelectedGameObject(button);
                    break;
                }
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

            ChipsSelected?.Invoke(new List<ChipSO>(_selectedChips));
            _selectedChips.Clear();
            EndChipSelection();
        }

        public void OnAddButtonPressed()
        {
            _maxAvailableChips += _selectedChips.Count();
            _maxAvailableChips = math.clamp(_maxSelectedChips, 0, _maxChipContainerSize);
            _selectedChips.Clear();
            ChipsSelected?.Invoke(null);
            EndChipSelection();
        }

        private void EndChipSelection()
        {
            _animationPlayer.SetTrigger("SlideOut");

            // Reset selected chip container images
            foreach (Transform child in _selectedChipsContainer.transform)
            {
                Image image = child.gameObject.GetComponent<Image>();
                if (image != null && image.sprite != _blankSprite)
                {
                    image.sprite = _blankSprite;
                }
            }
        }
    }
}
