using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerMovement2D _playerMovement2D;
        private PlayerState _playerState;
        
        public Sprite burntSprite;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMovement2D = GetComponent<PlayerMovement2D>();
            _animator.Play("Idle");
        }

        private void Start()
        {
            _playerState = PlayerState.Alive;
        }


        public void Move(InputAction.CallbackContext context)
        {
            if (_playerState == PlayerState.Dead)
            {
                return;
            }
            
            if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
            {
                _playerMovement2D.Move(context.ReadValue<Vector2>());
            }
            
            if (context.phase == InputActionPhase.Canceled)
            {
                _playerMovement2D.Move(Vector2.zero);
            }
        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (_playerState == PlayerState.Dead)
            {
                return;
            }
            
            if (context.phase == InputActionPhase.Performed)
            {
                _playerMovement2D.Jump();
            }
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
            GetComponent<SpriteRenderer>().sprite = burntSprite;
        }
        
        
    }
}


public enum PlayerState
{
    Alive,
    Dead
}
