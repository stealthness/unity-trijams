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
        private PlayerAnimator _playerAnimator;
        private bool _isDead = false;
        
        private bool _isKicking = false;
        private bool _isFalling = false;
        
        private void Awake()
        {
            _playerMovement2D = GetComponent<PlayerMovement2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
            _playerAnimator = GetComponent<PlayerAnimator>();
        }
        
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (_isKicking || _isDead)
            {
                return;
            }
            
            if (_isFalling)
            {
                //_playerAnimator.PlayAnimation(AnimationStrings.Fall);
                _playerMovement2D.OnFall();
                return;
            }
            
            if (context.phase == InputActionPhase.Canceled)
            {
                _playerMovement2D.OnMove(Vector2.zero);
                _playerAnimator.PlayAnimation(AnimationStrings.Idle);
                return;
            }

            if (context.phase == InputActionPhase.Performed)
            {
                const float tolerance = 0.001f;
                var direction = context.ReadValue<Vector2>();
                var xDir = (Math.Abs(direction.x) < tolerance)? 0: (direction.x < 0)? -1: 1;
                direction = new Vector2(xDir, 0);
                _playerAnimator.PlayAnimation(AnimationStrings.Walk);
                _playerMovement2D.OnMove(direction);                
            }
            
        }
        
        public void OnKick(InputAction.CallbackContext context)
        {
            if (_isDead)
            {
                return;
            }
            
            if (context.phase == InputActionPhase.Performed)
            {
                Debug.Log("Kick");
                _playerMovement2D.OnMove(Vector2.zero);
                _isKicking = true;
                _playerAnimator.PlayAnimation(AnimationStrings.Kick);
                Invoke(nameof(KickFinished), _playerAnimator.GetAnimationLength(AnimationStrings.Kick));
            }
        }
        
        private void KickFinished()
        {
            _isKicking = false;
            _playerAnimator.PlayAnimation(AnimationStrings.Idle);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Platform"))
            {
                Debug.Log("Falling");
                _playerMovement2D.OnFall();
                _isFalling = true;  
            }
        }

        public void OnHit()
        {
           _isDead = true;
           _playerAnimator.PlayAnimation(AnimationStrings.Burn);
           _playerMovement2D.OnMove(Vector2.zero);
        }
    }
}

