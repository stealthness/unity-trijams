using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Scripts.CorpEnemy
{
    public class BowlerMan : MonoBehaviour, IDamageable
    {
        public int health = 100;
        public Transform[] patrolPoints;
        private int currentPatrolIndex = 0;

        private void Update()
        {
            MovetToNextPatrolPoint();
        }

        private void MovetToNextPatrolPoint()
        {
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
            GetComponent<Rigidbody2D>().linearVelocityX =  patrolPoints[currentPatrolIndex].position.x - transform.position.x < 0  ? 1 : -1;
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