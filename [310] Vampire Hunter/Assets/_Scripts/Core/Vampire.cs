using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Core
{
    
    public class Vampire : NPC
    {
        
        
        
        private VampireState _state;
        private int _dir;
        private float _speed = 4f;
        
        [SerializeField] private float _batGravityForce = 0.3f;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            gameObject.tag = "Bat";
            _target = GameObject.FindWithTag("Player").transform;
            _state = VampireState.Bat;
            _rigidbody.gravityScale = _batGravityForce;
            _animator.Play("batflying");
            
        }
        
        private void Update()
        {
            switch (_state)
            {
                case VampireState.Bat:
                    break;
                case VampireState.Transforming:
                    break;
                case VampireState.Grounded:
                    break;
                case VampireState.Moving:
                    _rigidbody.linearVelocityX = _dir * _speed;
                    break;
                case VampireState.Burning:
                    break;
                case VampireState.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
 
        private void CheckDirection()
        {
            if (_target.position.x > transform.position.x)
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
            Debug.Log("Vampire burned");
            GameManger.Instance.UpdateScore(1);
            GetComponent<AudioSource>().Play();
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
            gameObject.tag = "Vampire";
            Invoke(nameof(AttackPlayer), 2f);
        }
        
        private void SetDead()
        {
            Destroy(gameObject);
        }
        
        private void BatDeath()
        {
            Destroy(this.gameObject);
        }


        private void AttackPlayer()
        {
            CheckDirection();
            _state = VampireState.Moving;
            _rigidbody.linearVelocityX = _dir * _speed;
        }

        private enum VampireState
         {
             Bat,
             Transforming,
             Grounded,
             Moving,
             Burning,
             Dead
         }
                 
    }
}