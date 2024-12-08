using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts
{
    public class Blob : MonoBehaviour
    {
        [SerializeField] private float _throwForce = 30f;


        // Start is called once before the first execution of Update after the MonoBehaviour is created


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                Vector2 dir = (player.transform.position - transform.position).normalized;
                player.Throw(dir * _throwForce);
                GetComponent<BoxCollider2D>().enabled = false;
                Invoke(nameof(delayedDestroy), 0.1f);
                
            }
        }
        
        private void delayedDestroy()
        {
            Destroy(gameObject);
        }
    }
}
