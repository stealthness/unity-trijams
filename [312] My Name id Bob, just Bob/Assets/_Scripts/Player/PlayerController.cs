using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement2D _playerMovement2D;
        private BoxCollider2D _boxCollider2D; 
        
        
        private void Awake()
        {
            _playerMovement2D = GetComponent<PlayerMovement2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
        }
        
        
        public void OnMove(InputAction.CallbackContext context)
        {
            
            if (context.phase == InputActionPhase.Canceled)
            {
                _playerMovement2D.OnMove(Vector2.zero);
                return;
            }

            if (context.phase == InputActionPhase.Performed)
            {
                const float tolerance = 0.001f;
                var direction = context.ReadValue<Vector2>();
                var xDir = (Math.Abs(direction.x) < tolerance)? 0: (direction.x < 0)? -1: 1;
                direction = new Vector2(xDir, 0);
                
                _playerMovement2D.OnMove(direction);                
            }
            

        }
    }
}

