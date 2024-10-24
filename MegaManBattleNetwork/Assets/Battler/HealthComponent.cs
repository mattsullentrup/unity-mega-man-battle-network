using System;
using TMPro;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class HealthComponent : MonoBehaviour
    {
        public static event Action<Battler> HealthDepleted;
        public int CurrentHealth => _currentHealth;

        [SerializeField] private int _maxHealth = 50;
        [SerializeField] private TextMeshProUGUI _healthText;
        private int _currentHealth = 5;
        private Battler _battler;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _battler = GetComponent<Battler>();
            _healthText.text = _currentHealth.ToString();
        }

        public void DecreaseHealth(int amount)
        {
            _currentHealth -= amount;
            ChangeHealth();
            if (_currentHealth == 0)
            {
                HealthDepleted?.Invoke(_battler);
            }
        }

        public void IncreaseHealth(int amount)
        {
            _currentHealth += amount;
            ChangeHealth();
        }

        private void ChangeHealth()
        {
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _healthText.text = _currentHealth.ToString();
        }
    }
}
