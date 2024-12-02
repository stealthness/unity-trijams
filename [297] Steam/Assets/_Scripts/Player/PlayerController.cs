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
        private AudioSource _audioSource;
        public AudioClip fixingClip;
        public AudioClip FireUp;
        [SerializeField] private bool isGrounded;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private float _jumpForce = 7f;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _audioSource = GetComponent<AudioSource>();
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
                _rb.linearVelocityX = _dir.x * _speed;
                CheckGrounded();
            }
            
        }

        private void CheckGrounded()
        {
            var center = _boxCollider2D.bounds.center;
            var size = _boxCollider2D.size;
            var hit = Physics2D.BoxCast(center, size , 0, Vector2.down, 0.05f, LayerMask.GetMask("Ground"));
            
            isGrounded = hit && hit.collider.CompareTag("Ground");
            
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
        
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (_playerState == PlayerState.Dead || _playerState == PlayerState.Fixing)
            {
                return;
            }
            
            if (context.phase == InputActionPhase.Performed && isGrounded)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
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
        

        public void ShowDeadPlayer()
        {
            _animator.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.GameOver();
        }
        
        public void FixPipe()
        {
            if (_playerState == PlayerState.Dead)
            {
                return;
            }
            Debug.Log("PC: Fix Pipe");   
            _playerState = PlayerState.Fixing;
            //_animator.Play("Fix");
            //var delay = _animator.GetCurrentAnimatorStateInfo(0).length;
            var delay = 3f;
            _audioSource.PlayOneShot(fixingClip);
            Invoke(nameof(EndFixing), delay);
        }
        
        public void EndFixing()
        {
            _audioSource.Stop();
            _playerState = PlayerState.Alive;
        }
        
        public void BurnPlayer()
        {
            Debug.Log("PC: Burn Player");
            _playerState = PlayerState.Dead;
            _animator.Play("Burn");
            DeadStop();
            var delay = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke(nameof(ShowDeadPlayer), delay);
            
        }

        private void DeadStop()
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void Throw(Vector2 dir, float pushBack)
        {
            Debug.Log("PC: Throw");
            _rb.AddForce(pushBack * dir, ForceMode2D.Impulse);
        }
    }
    
    
    public enum PlayerState
    {
        Alive,
        Dead,
        Fixing
    }
}