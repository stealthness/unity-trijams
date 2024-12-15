
using UnityEngine;

namespace _Scripts.CorpEnemy
{
    public class BowlerMan : MonoBehaviour, IDamageable
    {
        public int health = 100;
        public Transform[] patrolPoints;
        private int currentPatrolIndex = 0;
        private float _dir = 1f;
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void Update()
        {
            
            if (IsCloseToPatrolPoint())
            {
                MovetToNextPatrolPoint();
            }
            else
            {
                
            _rigidbody2D.linearVelocityX = _dir;
            }
        }

        private bool IsCloseToPatrolPoint()
        {
            return Mathf.Abs(patrolPoints[currentPatrolIndex].transform.position.x - transform.position.x) < 0.1f;
        }

        private void Start()
        {
            _dir = (transform.position.x - patrolPoints[currentPatrolIndex].position.x < 0) ? 1: -1;
        }
        

        private void MovetToNextPatrolPoint()
        {

            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            Debug.Log("currentPatrolIndex: " + currentPatrolIndex);
            _dir = patrolPoints[currentPatrolIndex].position.x - transform.position.x < 0  ? -1 : 1;
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