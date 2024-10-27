using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MegaManBattleNetwork
{
    public class GameManager : MonoBehaviour
    {
        public static event Action RoundEnding;
        public static GameManager Instance => _instance;
        public float ElapsedTime => _elapsedTime;
        public bool IsSelectingChips => _isSelectingChips;

        [SerializeField] private int _roundLength = 10;
        [SerializeField] private Button _restartButton;
        private float _elapsedTime;
        private bool _isRoundProgressing;
        private bool _isSelectingChips = true;
        private static GameManager _instance;

        private void Awake()
        {
            _instance = this;
            Time.timeScale = 0;
        }

        private void Start()
        {
            _restartButton.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            PlayerInput.PlayerPressedSelect += OnPlayerPressedSelect;
            ChipSelection.ChipsSelected += OnChipsSelected;
            HealthComponent.HealthDepleted += OnHealthComponentHealthDepleted;
        }

        private void OnDisable()
        {
            PlayerInput.PlayerPressedSelect -= OnPlayerPressedSelect;
            ChipSelection.ChipsSelected -= OnChipsSelected;
            HealthComponent.HealthDepleted -= OnHealthComponentHealthDepleted;
        }

        private void Update()
        {
            if (_isRoundProgressing)
            {
                _elapsedTime += Time.deltaTime;
            }
        }

        public void OnRestartButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnPlayerPressedSelect()
        {
            if (!_isRoundProgressing)
            {
                RoundEnding?.Invoke();
                _isSelectingChips = true;
                Time.timeScale = 0;
            }
        }

        private void OnChipsSelected(List<ChipSO> chips)
        {
            Time.timeScale = 1;
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

        private void OnHealthComponentHealthDepleted(Battler battler)
        {
            battler.Animation.SetTrigger("Die");
            var enemyManager = FindFirstObjectByType<EnemyManager>();
            var enemy = battler as Enemy;
            if (enemy != null)
            {
                enemyManager.Enemies.Remove(enemy);
            }

            if (battler is Player || enemyManager.Enemies.Count == 0)
            {
                EndTheGame();
            }

        }

        private void EndTheGame()
        {
            Debug.Log("ending the game");
            _restartButton.gameObject.SetActive(true);
        }
    }
}
