using UnityEngine;

namespace _Scripts.Core
{
    

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class Movement2D : MonoBehaviour
    {

        protected Rigidbody2D _rigidbody2D;
        protected Collider2D _collider2D;

        [SerializeField] protected float speed = 5f;

        protected virtual void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
        }
        
        
        protected virtual void OnMove(Vector2 direction)
        {
            _rigidbody2D.linearVelocity = direction * speed;
        }
    }
}
