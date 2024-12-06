using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int health = 100;
        
        public void TakeDamage(int damage)
        {
            
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            Debug.Log("Player took damage: " + damage + " health: " + health);
        }

        private void Die()
        {
            Debug.Log("Player is dead");
        }
    }
}
