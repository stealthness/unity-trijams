using System;
using UnityEngine;

namespace _Scripts.Core
{
    

 
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Movement2D : MonoBehaviour
    {

        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        [SerializeField] protected float speed = 5f;
        protected Vector2 _dir;


        private void Update()
        {
            transform.Translate(_dir  * (speed * Time.deltaTime));
        }

        protected internal virtual void OnMove(Vector2 direction)
        {
            _dir = direction;
            CheckDirection();
        }


        private void CheckDirection()
        {
            if (_dir.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_dir.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
    }
}
