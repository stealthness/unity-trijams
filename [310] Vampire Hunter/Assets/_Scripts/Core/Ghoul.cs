using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Core
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class Ghoul :  NPC
    {
        private GameObject _stars;
        
        [SerializeField] private int dir;
        [SerializeField] private float ghoulsSpeed = 1f;
        [SerializeField] private int ghoulsHealth = 3;
        [SerializeField] private bool isStunned = false;
        [SerializeField] private bool isDead = false;


        private void Awake()
        {
            base.Awake();
            _stars = transform.GetChild(0).gameObject;
            
        }
        
        private void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
            _animator.Play("Moving");
            isDead = false;
            CheckDirection();
        }
        
        
        private void Update()
        {
            if (isStunned)
            {
                return;
            }
            
            _rigidbody.linearVelocityX = dir * ghoulsSpeed;
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
            if (isDead)
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
            isStunned = true;
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
                isDead = true;
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
            isStunned = false;
            _animator.Play("Moving");
            _stars.SetActive(false);
        }
    }
}