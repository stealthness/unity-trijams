using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// This basic abstract Movement2D class for a 2D platformer
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Movement2D : MonoBehaviour
    {
        [SerializeField] internal Movement2DData stats;
        [SerializeField] protected bool isGrounded;
        protected Rigidbody2D rigidbody2D;
        protected internal SpriteRenderer spriteRenderer;
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


        /// <summary>
        /// changes the objects velocity to the new direction
        /// </summary>
        /// <param name="newDirection"></param>
        protected internal virtual void OnMove(Vector2 newDirection)
        {
            direction = newDirection;

            rigidbody2D.linearVelocity = isGrounded 
                ? stats.speed * new Vector2(direction.x, 0) 
                : new Vector2(stats.speed * direction.x, rigidbody2D.linearVelocity.y);

            CheckDirection();
        }

        /// <summary>
        /// Check the direction and flips the object sprite
        /// </summary>
        private void CheckDirection()
        {
            spriteRenderer.flipX = rigidbody2D.linearVelocityX switch
            {
                > 0 => false,
                < 0 => true,
                _ => spriteRenderer.flipX
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual void OnJump()
        {
            if (isGrounded)
            {
                rigidbody2D.AddForce(Vector2.up * stats.jumpForce, ForceMode2D.Impulse);
            }
        }
        
        private void Update()
        {
            CheckGrounded();
        }


        protected abstract void CheckGrounded();
    }
}
