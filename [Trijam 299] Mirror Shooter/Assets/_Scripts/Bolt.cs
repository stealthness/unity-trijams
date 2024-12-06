using System;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Bolt : MonoBehaviour
    {
        [SerializeField] private float boltSpeed = 10f;

        private void Update()
        {
            if (transform.position.y > 10f)
            {
                Destroy(this);
            }
            
            transform.position += Vector3.up * (boltSpeed * Time.deltaTime);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Rock"))
            {
                other.GetComponent<Rock>().TakeDamage(10);
            }
        }
    }
}