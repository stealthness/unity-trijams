
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PaperPlane : MonoBehaviour
    {
        [SerializeField] private float paperPlaneFlightTime = 0.6f;
        [SerializeField] private float paperPlaneGravityRate = 0.5f;

        private Rigidbody2D _rigidbody2D;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Invoke(nameof(GravityOn), paperPlaneFlightTime);
        }
        
        private void GravityOn()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.gravityScale = paperPlaneGravityRate;
            
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
        
    }
}
