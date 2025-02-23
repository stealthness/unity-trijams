using System;
using UnityEngine;

namespace _Scripts.Core
{
    
    public class Vampire : MonoBehaviour
    {
        
        
        
        private VampireState _state;
        private Animator _animator;
        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
        
        
        [SerializeField] private float _batGravityForce = 0.3f;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }


        private void Start()
        {
            _state = VampireState.Bat;
            _rigidbody.gravityScale = _batGravityForce;
            _animator.Play("batflying");
            
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
            if (_state == VampireState.Grounded && other.gameObject.CompareTag("Bolt"))
            {
                Burn();
                Destroy(other.gameObject);
            }       
            if (_state == VampireState.Bat && other.gameObject.CompareTag("Bolt"))
            {
                _state = VampireState.Dead;
                Invoke(nameof(BatDeath), 0.01f);
                Destroy(other.gameObject);
            }
        }

        private void Burn()
        {
            Debug.Log("Vampire burned");
            _state = VampireState.Burning;
            _animator.SetTrigger("Burn");
            _animator.Play("burn");
            _collider.isTrigger = true;
            _collider.name = "DeadVampire";
            _rigidbody.gravityScale = 0;
            Invoke(nameof(SetDead), 1f);
            
        }
        
        private void SetGrounded()
        {
            _animator.Play("idle");
            _state = VampireState.Grounded;
        }
        
        private void SetDead()
        {
            Destroy(gameObject);
        }
        
        private void BatDeath()
        {
            Destroy(this.gameObject);
        }


        private enum VampireState
         {
             Bat,
             Transforming,
             Grounded,
             Burning,
             Dead
         }
                 
    }
}