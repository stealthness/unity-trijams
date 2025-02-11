using _Scripts.Core;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Punk
{
    public class Punk : Movement2D
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Collision" + other.gameObject.name);
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().DamagePlayer();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Trigger" + other.gameObject.name);
            if (other.gameObject.CompareTag("Bag"))
            {
                Destroy(this);
            }
        }
    }
}
