using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerShip : MonoBehaviour
    {
        
        [SerializeField] private PlayerColor playerColor = PlayerColor.Red;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("CloudRed") || other.CompareTag("CloudBlue"))
            {
                switch (playerColor)
                {
                    case PlayerColor.Red:
                        if (other.CompareTag("CloudRed"))
                        {
                            GetComponentInParent<PlayerHealth>().TakeDamage(20);
                        }
                        break;
                    case PlayerColor.Blue:
                        if (other.CompareTag("CloudBlue"))
                        {
                            GetComponentInParent<PlayerHealth>().TakeDamage(20);
                        }
                        break;
                    default:
                        // do nothing
                    break;
                }
            }
        }
    }
}