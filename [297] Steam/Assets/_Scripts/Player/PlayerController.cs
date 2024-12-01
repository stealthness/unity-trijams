using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        
        Vector2 _dir;
        
        private void Start()
        {
            Debug.Log("Player Controller is ready");
        }
        
        private void Update()
        {
            transform.position += new Vector3(_dir.x, _dir.y, 0) * Time.deltaTime;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _dir = context.ReadValue<Vector2>();
            }

            if (context.canceled)
            {
                _dir = Vector2.zero;
            }
        }
    }
}