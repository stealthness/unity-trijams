using _Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Rock :MonoBehaviour
    {
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float maxDistanceSpawnFromCenter = 8f;

        private void Update()
        {
            
            
            if (transform.position.y < -6.5f)
            {
                var rx = Random.Range(-maxDistanceSpawnFromCenter,maxDistanceSpawnFromCenter);
                var ry = Random.Range(16f,28f);
                transform.position = new Vector3(rx, ry, 0);
            }

            transform.position += Vector3.down * (speed * Time.deltaTime);
            ;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FindObjectsByType<PlayerHealth>(0)[0].TakeDamage(10);
            }
        }
    }
}