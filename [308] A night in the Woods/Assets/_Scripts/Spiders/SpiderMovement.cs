using _Scripts.Core;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Spiders
{
    /// <summary>
    /// This class is responsible for the movement of the Spider in a top-down 2D game.
    /// </summary>
    public class SpiderMovement : Movement2DTopdown
    {
        public GameObject target;
        
        private Animator _animator;
        private const float KnockBackDistance = 3f;
        private int _knockBackCount = 0;


        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }


        protected override void Move()
        {
            var direction= Direction;
            if (target != null)
            {
                direction = target.transform.position - transform.position;
            }
 

            transform.position += direction.normalized * Time.deltaTime;
        }
        
        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }
        


        public void Stop()
        {
            Moving = false;
            _animator.Play("Dead");
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                FindAnyObjectByType<PlayerHealth>().TakeDamage(10);
                _animator.Play("Dead");
                KillSpider();
            }
            
        }


        public void KnockBack(Vector3 playerPosition)
        {
            _knockBackCount++;
            if (_knockBackCount > 3)
            {
                KillSpider();
                return;
            }
            
             // Set the desired knockback distance
            Vector3 direction = (transform.position - playerPosition).normalized;
            transform.position += direction * KnockBackDistance;
            
        }

        private void KillSpider()
        {
            Stop();
            Invoke (nameof(DestroySpider), 2f);
        }
        
       private void DestroySpider()
        {
            Destroy(gameObject);
        }
    }
}