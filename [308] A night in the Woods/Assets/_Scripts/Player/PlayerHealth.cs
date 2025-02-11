
using TMPro;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public TextMeshProUGUI healthText;
        
        private PlayerController _playerController;
        
        private int _health = 100;
        private int _maxHealth = 100;
        private bool _isDead = false;
        private bool _isPoisoned = false;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }


        public void TakeDamage(int damage)
        {
            if (_isDead) return;
            _health -= damage;
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
            healthText.text = $"{_health}";
        }

        private void Die()
        {
            _playerController.KillPlayer();
            GameManager.Instance.GameOver();
        }
    }
}