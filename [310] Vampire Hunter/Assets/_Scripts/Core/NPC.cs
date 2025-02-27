using UnityEngine;

namespace _Scripts.Core
{
    public class NPC : MonoBehaviour
    {
        protected Animator _animator;
        protected BoxCollider2D _collider;
        protected Rigidbody2D _rigidbody;
        protected SpriteRenderer _spriteRenderer;
        protected Transform _target;
        protected AudioSource _audioSource;
        protected int _dir;
        
        
        
        protected void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        
        protected void CheckDirection()
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
    }
}