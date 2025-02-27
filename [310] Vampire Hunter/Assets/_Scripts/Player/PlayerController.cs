using System;
using _Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerSideways2DMovement))]
    public class PlayerController : MonoBehaviour
    {

        public GameObject Panel;

        [SerializeField] private bool onFireCooldown = false;
        [SerializeField] private bool startInCutScene = false;
        private const float Tol = 0.0001f;
        private const float FireCooldownTime = 0.4f;

        private PlayerSideways2DMovement _player;
        private PlayerState _state = PlayerState.Alive;

        private void Awake()
        {
            _player = GetComponent<PlayerSideways2DMovement>();
        }
        
        
        private void Start()
        {
            if (!startInCutScene) return;
            
            _state = PlayerState.CutScene;
            _player.Stop();
            onFireCooldown = true;
        }

        public void OnMove(InputAction.CallbackContext context)
        {

            if (_state != PlayerState.Alive) return;
            
            if (context.performed)
            {
                var moveX = context.ReadValue<Vector2>().x;
                _player.SetDirection(Mathf.Abs(moveX) > Tol ? Mathf.Sign(moveX) : 0);
            }
            
            if (context.canceled)
            {
                _player.SetDirection(0);
            }
        }
        
        public void OnFire(InputAction.CallbackContext context)
        {
            if (_state != PlayerState.Alive || onFireCooldown || !context.performed) return;
            
            GetComponent<AudioSource>().Play();
            _player.Fire();
            onFireCooldown = true;
            Invoke(nameof(FireCooldownOver), FireCooldownTime);
        }

        private void FireCooldownOver()
        {
            onFireCooldown = false;
        }
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Vampire") && !other.gameObject.CompareTag("Ghoul")) return;
            
            
            Panel.gameObject.SetActive(true);
            GameManger.Instance.GameOver();
            _state = PlayerState.Dead;
            _player.enabled = false;
            Time.timeScale = 0;
        }
        
        
        private enum PlayerState
        {
            Alive,
            CutScene,
            Dead
        }
    }
}