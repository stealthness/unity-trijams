
using UnityEngine;

namespace _Scripts.Managers
{
    
    [RequireComponent(typeof(BoxCollider2D))]
    public class AreaCheck : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Object left the area" + other.name);
            Destroy(other.gameObject);
        }
    }
}