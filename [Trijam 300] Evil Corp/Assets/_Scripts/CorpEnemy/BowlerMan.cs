using UnityEngine;

namespace _Scripts.CorpEnemy
{
    public class BowlerMan : MonoBehaviour, IDamageable
    {
        public int health = 100;
        
        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}