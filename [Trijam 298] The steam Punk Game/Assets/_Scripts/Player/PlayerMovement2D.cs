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
        

        }

        protected override void CheckGrounded()
        {
            var center = _boxCollider2D.bounds.center;
            var size = _boxCollider2D.size;
            var hit = Physics2D.BoxCast(center, size , 0, Vector2.down, 0.05f, ~PlayerStats.playerLayer);
        
            isGrounded = hit && hit.collider.CompareTag("Ground");
            
        
        }
    }
}
