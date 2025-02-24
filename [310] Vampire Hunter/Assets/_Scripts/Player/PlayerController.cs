using System;
using _Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerSideways2DMovement))]
    public class PlayerController : MonoBehaviour
    {

        public GameObject Panel;
        
        private PlayerSideways2DMovement _player;

        private const float TOL = 0.0001f;
        private PlayerState _state = PlayerState.Alive;
        private bool _onFireCooldown = false;
        private float _fireCooldown = 0.2f;

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
            if (_state == PlayerState.Dead || _onFireCooldown)
            {
                return;
            }
            
            
            if (context.performed)
            {
                GetComponent<AudioSource>().Play();
                Debug.Log("Fire");
                _player.Fire();
                _onFireCooldown = true;
                Invoke(nameof(FireCooldown), _fireCooldown);
            }
        }

        private void FireCooldown()
        {
            _onFireCooldown = false;
        }
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Vampire") || other.gameObject.CompareTag("Ghoul"))
            {
                Panel.gameObject.SetActive(true);
                GameManger.Instance.GameOver();
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