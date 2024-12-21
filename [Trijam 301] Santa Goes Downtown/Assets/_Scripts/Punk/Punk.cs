using _Scripts.Core;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Punk
{
    public class Punk : Movement2D
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().DamagePlayer();
            }
        }
    }
}
