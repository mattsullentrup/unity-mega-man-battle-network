using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class GameManager : MonoBehaviour
    {
        public static event Action RoundEnding;
        [SerializeField] private int _roundLength = 10;
        private bool _isRoundProgressing;

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

        private void OnPlayerPressedSelect()
        {
            if (!_isRoundProgressing)
            {
                RoundEnding?.Invoke();
            }
        }

        private void OnChipsSelected(List<ChipSO> chips)
        {
            StartCoroutine(RoundRoutine());
        }

        private IEnumerator RoundRoutine()
        {
            _isRoundProgressing = true;
            yield return new WaitForSeconds(_roundLength);
            _isRoundProgressing = false;
        }
    }
}
