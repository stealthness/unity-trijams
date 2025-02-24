using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Core
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Ghoul : MonoBehaviour
    {
        public Transform target;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private AudioSource _audioSource;
        
        private GameObject _stars;
        
        
        [SerializeField] private int dir;
        [SerializeField] private float ghoulsSpeed = 1f;
        [SerializeField] private int ghoulsHealth = 3;
        [SerializeField] private bool _stunned = false;
        [SerializeField] private bool _isDead = false;


        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
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
            
            _rigidbody.linearVelocityX = dir * ghoulsSpeed;
        }

        private void CheckDirection()
        {
            if (target.position.x > transform.position.x)
            {
                dir = 1;
                _spriteRenderer.flipX = true;
            }
            else
            {
                dir = -1;
                _spriteRenderer.flipX = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Ghoul hit player");
                GameManger.Instance.GameOver();
                
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDead)
            {
                return;
            }            
            
            if (other.gameObject.CompareTag("Bolt"))
            {
                Debug.Log("Ghoul hit by bolt");
                Destroy(other.gameObject);
                StunGhoul();
                Invoke(nameof(StunCooldown), 2f);
            }
        }

        private void StunGhoul()
        {
            _stunned = true;
            _animator.Play("Stun");
            _stars.SetActive(true);
            _audioSource.Play();
            GhoulsHealthHit();
        }

        private void GhoulsHealthHit()
        {
            ghoulsHealth--;
            if (ghoulsHealth <= 0)
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