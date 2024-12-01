
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class Lava : MonoBehaviour
    {
        public UnityEvent _playerDeathByLAva;
        


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerDeathByLAva.Invoke();
            }
        }
    }
}
