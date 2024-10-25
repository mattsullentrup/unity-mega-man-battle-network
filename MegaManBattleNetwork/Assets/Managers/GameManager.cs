using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class GameManager : MonoBehaviour
    {
        public static event Action RoundEnding;
        public static GameManager Instance => _instance;
        public float ElapsedTime => _elapsedTime;
        public bool IsSelectingChips => _isSelectingChips;

        private static GameManager _instance;
        [SerializeField] private int _roundLength = 10;
        private bool _isRoundProgressing;
        private float _elapsedTime;
        private bool _isSelectingChips = true;

        private void Awake()
        {
            _instance = this;
        }

        private void OnEnable()
        {
            PlayerInput.PlayerPressedSelect += OnPlayerPressedSelect;
            ChipSelection.ChipsSelected += OnChipsSelected;
        }

        private void OnDisable()
        {
            PlayerInput.PlayerPressedSelect -= OnPlayerPressedSelect;
            ChipSelection.ChipsSelected -= OnChipsSelected;
        }

        private void Update()
        {
            if (_isRoundProgressing)
            {
                _elapsedTime += Time.deltaTime;
            }
        }

        private void OnPlayerPressedSelect()
        {
            if (!_isRoundProgressing)
            {
                RoundEnding?.Invoke();
                _isSelectingChips = true;
                // Time.timeScale = 0;
            }
        }

        private void OnChipsSelected(List<ChipSO> chips)
        {
            StartCoroutine(RoundRoutine());
            _isSelectingChips = false;
        }

        private IEnumerator RoundRoutine()
        {
            _elapsedTime = 0;
            _isRoundProgressing = true;
            yield return new WaitForSeconds(_roundLength);
            _isRoundProgressing = false;
        }

        // public static void ToggleBattlers(bool value)
        // {
        //     IToggleable[] toggleables = FindObjectsByType<MonoBehaviour>(
        //             FindObjectsInactive.Include, FindObjectsSortMode.None
        //             ).OfType<IToggleable>().ToArray();
        //     foreach (var toggleable in toggleables)
        //     {
        //         toggleable.Toggle(value);
        //     }
        // }
    }
}
