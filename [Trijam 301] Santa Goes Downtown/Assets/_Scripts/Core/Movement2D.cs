using _Scripts.Player;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Movement2D : MonoBehaviour
    {
        [SerializeField] internal Movement2DData stats;
        [SerializeField] protected bool isGrounded;
        protected Rigidbody2D rigidbody2D;
        protected SpriteRenderer spriteRenderer;
        protected Vector2 direction;

        protected virtual void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            rigidbody2D.linearVelocity = new Vector2(stats.speed * direction.x, rigidbody2D.linearVelocity.y);
        }


        protected internal virtual void OnMove(Vector2 direction)
        {
            this.direction = direction;

            rigidbody2D.linearVelocity = isGrounded 
                ? stats.speed * new Vector2(direction.x, 0) 
                : new Vector2(stats.speed * direction.x, rigidbody2D.linearVelocity.y);

            CheckDirection();
        }

        private void CheckDirection()
        {
            spriteRenderer.flipX = rigidbody2D.linearVelocityX switch
            {
                > 0 => false,
                < 0 => true,
                _ => spriteRenderer.flipX
            };
        }

        protected internal virtual void OnJump()
        {
            if (isGrounded)
            {
                rigidbody2D.AddForce(Vector2.up * stats.jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
