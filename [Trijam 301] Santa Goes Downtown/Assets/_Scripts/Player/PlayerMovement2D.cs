using _Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Player
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement2D : MonoBehaviour
    {
        
        public PlayerMovement2DData stats;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private BoxCollider2D _box;
        private Vector2 _dir;
        [SerializeField] private bool isGrounded;
        [SerializeField] private int currentJumpCount;

        [SerializeField] private float tolerance = 0.1f;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _sr = GetComponent<SpriteRenderer>();
            _box = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            isGrounded = true;
            currentJumpCount = stats.maxJumps;
        }

        private void Update()
        {
            CheckGrounded();
        }
        
        /// <summary>
        /// Jump is called in the PlayerController script. It makes the player jump.
        /// </summary>
        public void Jump()
        {
            Debug.Log("PlayerMovement2D: Jump");
            
            if (!isGrounded && currentJumpCount <= 0 || _rb.linearVelocity.y < -tolerance)
            {
                
                return;
            }
            
            if (isGrounded)
            {
                currentJumpCount = stats.maxJumps;
            }
            else
            {
                currentJumpCount--;    
            }
            
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * stats.jumpForce, ForceMode2D.Impulse);

        }
        
        /// <summary>
        /// CheckDirection is called in the OnMove method. It checks the direction of the player and flips the sprite accordingly.
        /// </summary>
        private void CheckDirection()
        {
            _sr.flipX = _rb.linearVelocityX switch
            {
                > 0 => false,
                < 0 => true,
                _ => _sr.flipX
            };
        }
        
        /// <summary>
        /// OnMove is called in the PlayerController script. It moves the player in the direction of the input.
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector2 direction)
        {
            _dir = direction;

            _rb.linearVelocity = isGrounded 
                ? stats.speed * new Vector2(direction.x, 0) 
                : new Vector2(stats.speed * direction.x, _rb.linearVelocity.y);

            CheckDirection();
        }
        
        /// <summary>
        /// CheckGrounded is called in the Update method. It checks if the player is grounded.
        /// </summary>
        private void CheckGrounded()
        {
            var center = _box.bounds.center;
            var size = _box.size;
            var hit = Physics2D.BoxCast(center, size , 0, Vector2.down, 0.05f, ~stats.playerLayer);
            
            isGrounded = hit && hit.collider.CompareTag("Ground");
            
        }

        /// <summary>
        /// DeadStop is called when the player dies. It stops the player from moving and falling.
        /// </summary>
        public void DeadStop()
        {
            _dir = Vector2.zero;
            _rb.gravityScale = 0;
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
