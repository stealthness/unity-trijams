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

        private void Awake()
        {
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
    }
    
    
    public enum PlayerState
    {
        Alive,
        Dead,
        Fixing
    }
}