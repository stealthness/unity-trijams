
using System;
using UnityEngine;

namespace _Scripts.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerSideways2DMovement : MonoBehaviour
    {
        private const float PlayerSpeed = 2f;
        private const float BoltSpeed = 16f;
        
        private Collider2D _collider;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private float _dir = 0;

        public GameObject boltPrefab;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }


        private void Start()
        {
            _spriteRenderer.flipX = false;
        }

        private void Update()
        {
            if (_dir == 0)
            {
                Stop();
                return;
            }
            _rigidbody.linearVelocityX = _dir * PlayerSpeed;
        }


        public void SetDirection(float dir)
        {
            _dir = dir;
            
            if (_dir == 0)
            {
                Stop();
                return;
            }
            
            _animator.Play("Walk");
            _dir = dir * PlayerSpeed;
            CheckDirection();
        }

        
        public void CheckDirection()
        {
            if (_dir > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_dir < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        public void Fire()
        {
            var boltDir = _spriteRenderer.flipX ? -1 : 1;
            var offset = _spriteRenderer.flipX ? new Vector3(-1, 0.5f, 0): new Vector3(1, 0.5f, 0);
            var bolt = Instantiate(boltPrefab, transform.position + offset, Quaternion.identity);                                    
            if (_spriteRenderer.flipX)
            {
                bolt.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                bolt.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            bolt.GetComponent<Rigidbody2D>().linearVelocityX = boltDir * BoltSpeed;
        }

        public void Stop()
        {
            _rigidbody.linearVelocityX = 0;
            _animator.Play("Idle");
        }
    }
}