using _Scripts.Core;
using _Scripts.Spiders;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerController : MonoBehaviour
    {
        public Color SickGreen = new Color(0.5f, 1f, 0.5f, 1f);
        public AudioClip kickSound;
        public AudioClip screamSound;
        public AudioClip pickUpSound;
        public CampFire campFire;
        
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private PlayerAnimator _playerAnimator;
        private AudioSource _audioSource;
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private Vector2 direction;
        private bool _isDead = false;
        private bool _isKicking = false;
        private const float Tol = 0.01f;
        private float KickCooldown = 0.6f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _playerAnimator = GetComponent<PlayerAnimator>();
            _audioSource = GetComponent<AudioSource>();
            _sr = GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            _playerAnimator.PlayAnimation(PlayerAnimation.Idle);
        }

        /// <summary>
        /// OnMove is called when the player moves
        /// </summary>
        /// <param name="context"></param>
        public void OnMove(InputAction.CallbackContext context)
        {
            
            if (_isDead || _isKicking) return;
            
            if (context.performed)
            {
                direction = context.ReadValue<Vector2>();
                ChangeDirection();
                _playerAnimator.PlayAnimation(PlayerAnimation.Moving);
                
                
            }
            if (context.canceled)
            {
                direction = Vector2.zero;
                _playerAnimator.PlayAnimation(PlayerAnimation.Idle);
            }
        }

        private void LateUpdate()
        {
            if (_isKicking || _isDead)
            {
                _rb.linearVelocity = Vector2.zero;
                return;
            }
            
            _rb.linearVelocity = speed * new Vector2(direction.x, direction.y);
        }

        /// <summary>
        ///  Change the direction of the sprite as per the direction of the player
        /// </summary>
        void ChangeDirection()
        {
            if (direction.x < -Tol)
            {
             _sr.flipX = true;
            }
            if (direction.x > Tol)
            {
             _sr.flipX = false;
            }               
        }

        public void KillPlayer()
        {
            _isDead = true;
            _rb.linearVelocity = Vector2.zero;
            _audioSource.PlayOneShot(screamSound);
            _sr.color = SickGreen;
            _playerAnimator.PlayAnimation(PlayerAnimation.Dead);
        }
        
        
        public void OnKick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("Kick");
                _isKicking = true;
                _audioSource.PlayOneShot(kickSound);
                CheckForSpidersInRange();
                _playerAnimator.PlayAnimation(PlayerAnimation.Kick);
                Invoke(nameof(DelayedKickCooldown), _playerAnimator.GetClipLength(PlayerAnimation.Kick));
            }
            
        }
        
        private void CheckForSpidersInRange()
        {
            float radius = 2f;
            foreach (var hit in Physics2D.OverlapCircleAll(transform.position, radius))
            {
                hit.GetComponent<SpiderMovement>()?.KnockBack(transform.position);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }
        
        private void DelayedKickCooldown()
        {
            _isKicking = false;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Stick"))
            {
                Debug.Log("Player picked up stick by stick");
                _audioSource.PlayOneShot(pickUpSound);
                campFire.AddStick(10);
                Destroy(other.gameObject);
            }
        }
    }
}