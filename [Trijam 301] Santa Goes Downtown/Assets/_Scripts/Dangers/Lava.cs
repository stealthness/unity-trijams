using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Dangers
{
    public class Lava : MonoBehaviour
    {
        public UnityEvent burnPlayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player hits Lava and burns");
                burnPlayer.Invoke();
            }
        }
    }
}
