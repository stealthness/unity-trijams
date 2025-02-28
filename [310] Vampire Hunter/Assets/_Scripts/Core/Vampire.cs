using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// This class is used to control the Vampire NPC.
    /// </summary>
    public class Vampire : NPC
    {
        
        private VampireState _state;
        [SerializeField] private float speed = 4f;
        [SerializeField] private float batGravityForce = 0.3f;
        
        
        private void Start()
        {
            gameObject.tag = "Bat";
            _target = GameObject.FindWithTag("Player").transform;
            _state = VampireState.Bat;
            _rigidbody.gravityScale = batGravityForce;
            _animator.Play("batflying");
            
        }
        
        private void LateUpdate()
        {
            if (_state == VampireState.Moving)
            {
                _rigidbody.linearVelocityX = _dir * speed;
            }
            if (_state is VampireState.Burning or VampireState.Transforming)
            {
                _rigidbody.linearVelocityX = 0;
            }
        }
        


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_state == VampireState.Bat && other.gameObject.CompareTag("Ground"))
            {
                _state = VampireState.Transforming;
                _rigidbody.gravityScale = 1;
                _animator.Play("transform");
                Invoke(nameof(SetGrounded), 0.2f);
            }
            

            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_state is VampireState.Grounded or VampireState.Moving && other.gameObject.CompareTag("Bolt"))
            {
                Burn();
                Destroy(other.gameObject);
            }    
        }

        private void Burn()
        {
            GameManger.Instance.UpdateScore(1);
            _state = VampireState.Burning;
            _audioSource.Play();
            _animator.Play("burn");
            _collider.isTrigger = true;
            _collider.name = "DeadVampire";
            _rigidbody.gravityScale = 0;
            _rigidbody.linearVelocity = Vector2.zero;
            Invoke(nameof(SetDead), 1f);
            
        }
        
        private void SetGrounded()
        {
            _animator.Play("idle");
            _state = VampireState.Grounded;
            gameObject.tag = "Vampire";
            Invoke(nameof(AttackPlayer), 0.5f);
        }
        
        private void SetDead()
        {
            Destroy(gameObject);
        }
        
        private void BatDeath()
        {
            Destroy(gameObject);
        }


        private void AttackPlayer()
        {
            CheckDirection();
            _state = VampireState.Moving;
            _rigidbody.linearVelocityX = _dir * speed;
        }

        private enum VampireState
         {
             Bat,
             Transforming,
             Grounded,
             Moving,
             Burning,
         }
                 
    }
}