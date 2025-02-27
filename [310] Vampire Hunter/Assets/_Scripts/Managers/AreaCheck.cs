using UnityEngine;

namespace _Scripts.Managers
{
    
    /// <summary>
    /// This script is used to check if an object has left a certain area and then destroys it.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class AreaCheck : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}