using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        public GameObject paperPlanePrefab;
        private bool _onShootCooldown = false;
        private float _cooldownDelay = 0.5f;
        [SerializeField] private Vector3 offset = 0.5f * Vector3.up;

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed && !_onShootCooldown)
            {
                Shoot();
            }

        }

        private void Shoot()
        {
            _onShootCooldown = true;
            var plane = Instantiate(paperPlanePrefab, transform.position + offset, Quaternion.identity);
            bool flipX = GetComponent<SpriteRenderer>().flipX;
            plane.GetComponent<SpriteRenderer>().flipX = flipX;
            plane.GetComponent<Rigidbody2D>().linearVelocityX = (flipX ? -1 : 1) * 10f;
            Invoke(nameof(CooldownDelay), _cooldownDelay);
        }
        
        private void CooldownDelay()
        {
            _onShootCooldown = false;
        }
    }
}
