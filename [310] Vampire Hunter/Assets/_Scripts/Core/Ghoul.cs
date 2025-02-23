using System;
using UnityEngine;

namespace _Scripts.Core
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Ghoul : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private GameObject _stars;
        private int _dir;
        
        public Transform target;
        [SerializeField] private float _ghoulSpeed = 1f;
        [SerializeField] private int _ghoulHealth = 3;
        private bool _stunned = false;
        private bool _isDead = false;


        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _stars = transform.GetChild(0).gameObject;
            
        }
        
        private void Start()
        {
            if (_stunned)
            {
                return;
            }
            _animator.Play("Moving");
            
            _isDead = false;
            CheckDirection();
        }
        
        
        private void Update()
        {
            if (_stunned)
            {
                return;
            }
            
            _rigidbody.linearVelocityX = _dir * _ghoulSpeed;
        }

        private void CheckDirection()
        {
            if (target.position.x > transform.position.x)
            {
                _dir = 1;
                _spriteRenderer.flipX = true;
            }
            else
            {
                _dir = -1;
                _spriteRenderer.flipX = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Ghoul hit player");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bolt"))
            {
                Destroy(other.gameObject);
                if (_isDead)
                {
                    return;
                }
                
                Debug.Log("Ghoul hit by bolt");
                _stunned = true;
                _animator.Play("Stun");
                _stars.SetActive(true);
                
                GhoulsHealthHit();
                
                Invoke(nameof(StunCooldown), 2f);   
                
            }
        }

        private void GhoulsHealthHit()
        {
            _ghoulHealth--;
            if (_ghoulHealth <= 0)
            {
                _isDead = true;
                Die();
                
            }
        }

        private void Die()
        {
            _animator.Play("Die");
            _rigidbody.linearVelocityX = 0;
            _rigidbody.gravityScale = 0;
            _collider.isTrigger = true;
            Invoke(nameof(DelayedDeath), 1f);
        }


        private void DelayedDeath()
        {
            _animator.Play("Dead");
            _spriteRenderer.enabled = false;
            Destroy(this.gameObject);
        }
        
        private void StunCooldown()
        {
            _stunned = false;
            _animator.Play("Moving");
            _stars.SetActive(false);
        }
    }
}