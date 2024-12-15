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
            if (IsCloseToPatrolPoint())
            {
                MovetToNextPatrolPoint();
            }
            //MovetToNextPatrolPoint();
        }

        private bool IsCloseToPatrolPoint()
        {
            //Debug.Log("Distance: " + Mathf.Abs(patrolPoints[currentPatrolIndex].transform.position.x - transform.position.x));
            return Mathf.Abs(patrolPoints[currentPatrolIndex].transform.position.x - transform.position.x) < 0.1f;
        }

        private void Start()
        {
            int dir = (transform.position.x - patrolPoints[currentPatrolIndex].position.x < 0) ? 1: -1;
            GetComponent<Rigidbody2D>().linearVelocityX = dir;
        }
        

        private void MovetToNextPatrolPoint()
        {

            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            Debug.Log("currentPatrolIndex: " + currentPatrolIndex);
            GetComponent<Rigidbody2D>().linearVelocityX =  patrolPoints[currentPatrolIndex].position.x - transform.position.x < 0  ? -1 : 1;
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