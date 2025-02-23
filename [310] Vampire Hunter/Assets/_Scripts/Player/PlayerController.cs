using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerSideways2DMovement))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerSideways2DMovement _player;

        private const float TOL = 0.0001f;
        private PlayerState _state = PlayerState.Alive;

        private void Awake()
        {
            _player = GetComponent<PlayerSideways2DMovement>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {

            if (_state == PlayerState.Dead)
            {
                return;
            }
            
            
            if (context.performed)
            {
                var moveX = context.ReadValue<Vector2>().x;
                _player.SetDirection(Mathf.Abs(moveX) > TOL ? Mathf.Sign(moveX) : 0);
            }
            
            if (context.canceled)
            {
                _player.SetDirection(0);
            }
        }
        
        public void OnFire(InputAction.CallbackContext context)
        {
            if (_state == PlayerState.Dead)
            {
                return;
            }
            
            
            if (context.performed)
            {
                Debug.Log("Fire");
                _player.Fire();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Vampire"))
            {
                _state = PlayerState.Dead;
                _player.enabled = false;
                Time.timeScale = 0;
            }
        }
        
        
        private enum PlayerState
        {
            Alive,
            Dead
        }
    }
}