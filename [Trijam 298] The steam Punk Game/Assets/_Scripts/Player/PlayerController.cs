using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement2D _playerMovement2D;
        private PlayerState _playerState;
        private Animator _animator;
        private AudioSource _audioSource;
        public AudioClip fixingClip;
        public Sprite playerSprite;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();   
            _playerMovement2D = GetComponent<PlayerMovement2D>();
            _audioSource = GetComponent<AudioSource>();
            
        }

        private void Start()
        {
            _playerState = PlayerState.Alive;
            _playerMovement2D.spriteRenderer.sprite = playerSprite;
        }


        public void Move(InputAction.CallbackContext context)
        {
            if (_playerState == PlayerState.Dead || _playerState == PlayerState.Fixing)
            {
                return;
            }
            
            if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
            {
                _playerMovement2D.OnMove(context.ReadValue<Vector2>());
            }
            
            if (context.phase == InputActionPhase.Canceled)
            {
                _playerMovement2D.OnMove(Vector2.zero);
            }
        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (_playerState == PlayerState.Dead || _playerState == PlayerState.Fixing)
            {
                return;
            }
            
            if (context.phase == InputActionPhase.Performed)
            {
                _playerMovement2D.OnJump();
            }
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
            _playerMovement2D.DeadStop();
            var delay = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke(nameof(ShowDeadPlayer), delay);
            
        }

        public void ShowDeadPlayer()
        {
            _animator.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
    
    public enum PlayerState
     {
         Alive,
         Dead,
         Fixing
     }
}





