using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement2D : Movement2D
    {
        private BoxCollider2D _boxCollider2D;
        private PlayerController _playerController;
        public PlayerMovement2DData PlayerStats;


        protected override void Awake()
        {
            base.Awake();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _playerController = GetComponent<PlayerController>();
            spriteRenderer.sprite = _playerController.playerSprite;
        

        }

        protected override void CheckGrounded()
        {
            var center = _boxCollider2D.bounds.center;
            var size = _boxCollider2D.size;
            var hit = Physics2D.BoxCast(center, size , 0, Vector2.down, 0.05f, ~PlayerStats.playerLayer);
        
            isGrounded = hit && hit.collider.CompareTag("Ground");
            
        
        }
        
        /// <summary>
        /// DeadStop is called when the player dies. It stops the player from moving and falling.
        /// </summary>
        public void DeadStop()
        {
            direction = Vector2.zero;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}
