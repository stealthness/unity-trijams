
using UnityEngine;

namespace _Scripts.CorpEnemy
{
    public class BowlerMan : MonoBehaviour, IDamageable
    {
        public Transform[] patrolPoints;
        
        [ SerializeField] private int health = 100;
        private int _currentPatrolIndex = 0;
        private float _dirX = 1f;
        
        private const float Tol = 0.1f;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void Update()
        {
            if (IsCloseToPatrolPoint())
            {
                MoveToNextPatrolPoint();
            }
            else
            {
                _rb.linearVelocityX = _dirX;
            }
        }

        private bool IsCloseToPatrolPoint()
        {
            return Mathf.Abs(patrolPoints[_currentPatrolIndex].transform.position.x - transform.position.x) < Tol;
        }

        private void Start()
        {
            _dirX = (transform.position.x - patrolPoints[_currentPatrolIndex].position.x < 0) ? 1: -1;
        }
        

        private void MoveToNextPatrolPoint()
        {

            _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
            _dirX = patrolPoints[_currentPatrolIndex].position.x - transform.position.x < 0  ? -1 : 1;
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