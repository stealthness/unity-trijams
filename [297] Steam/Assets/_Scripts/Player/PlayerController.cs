using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        
        Vector2 _dir;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        [SerializeField] private float _speed = 5f;
        private PlayerState _playerState = PlayerState.Alive;
        private Animator _animator;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.freezeRotation = true;
            _sr = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Debug.Log("Player Controller is ready");
            _playerState = PlayerState.Alive;
        }
        
        private void Update()
        {
            if (_playerState == PlayerState.Alive)
            {
                transform.position += new Vector3(_dir.x, 0, 0) * (_speed * Time.deltaTime);
            }
            
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (_playerState == PlayerState.Dead || _playerState == PlayerState.Fixing)
            {
                return;
            }
            
            if (context.performed)
            {
                _dir = context.ReadValue<Vector2>();
                CheckDirection();
            }

            if (context.canceled)
            {
                _dir = Vector2.zero;
            }
        }
        
        private void CheckDirection()
        {
            
            if (_dir.x > 0)
            {
                _sr.flipX = false;
            }
            else if (_dir.x < 0)
            {
                _sr.flipX = true;
            }
        }
        
        public void BurnPlayer()
        {
            Debug.Log("PC: Burn Player");
            _playerState = PlayerState.Dead;
            _animator.Play("Burn");
            _rb.linearVelocity = Vector2.zero;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            var delay = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke(nameof(ShowDeadPlayer), delay);
            
        }
        public void ShowDeadPlayer()
        {
            _animator.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.GameOver();
        }
    }
    
    
    public enum PlayerState
    {
        Alive,
        Dead,
        Fixing
    }
}