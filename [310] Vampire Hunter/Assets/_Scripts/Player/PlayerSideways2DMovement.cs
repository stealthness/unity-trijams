
using UnityEngine;

namespace _Scripts.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerSideways2DMovement : MonoBehaviour
    {
        private const float PLAYER_SPEED = 3f;
        private const float BOLT_SPEED = 16f;
        
        Collider2D _collider;
        Rigidbody2D _rigidbody;
        SpriteRenderer _spriteRenderer;
        float _dir = 0;

        public GameObject boltPrefab;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _rigidbody.linearVelocityX = _dir * PLAYER_SPEED;
        }


        public void SetDirection(float dir)
        {
            _dir = dir * PLAYER_SPEED;
            CheckDirection();
        }

        
        public void CheckDirection()
        {
            if (_dir > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_dir < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        public void Fire()
        {
            var boltDir = _spriteRenderer.flipX ? -1 : 1;
            var offset = _spriteRenderer.flipX ? new Vector3(-1, 0.5f, 0): new Vector3(1, 0.5f, 0);
            var bolt = Instantiate(boltPrefab, transform.position + offset, Quaternion.identity);                                    
            if (_spriteRenderer.flipX)
            {
                bolt.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                bolt.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            bolt.GetComponent<Rigidbody2D>().linearVelocityX = boltDir * BOLT_SPEED;
        }
    }
}