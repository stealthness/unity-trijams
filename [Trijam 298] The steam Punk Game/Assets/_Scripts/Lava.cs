using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class Lava : MonoBehaviour
    {
        
        public UnityEvent onPlayerTouchedLava;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player has touched the lava");
                onPlayerTouchedLava.Invoke();
                
            }
        }   
    }
}
