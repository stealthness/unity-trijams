using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject boltPrefab;
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float x;
        private GameObject _leftShip;
        private GameObject _rightShip;
        [SerializeField] private bool leftShipActiveControl = true;
        [SerializeField] private int sign = 1;
        [SerializeField] private float actionCoolDownTimer = 3f;
        [SerializeField] private bool actionCooldown;
        [SerializeField] private bool fireCooldown;
        [SerializeField] private float fireCoolDownTimer = 0.5f;

        private void Start()
        {
            Debug.Log("Player Controller is ready");
            _leftShip = GameObject.Find("LeftShip");
            _rightShip = GameObject.Find("RightShip");
        }

        private void Update()
        {
            _leftShip.transform.position += new Vector3(sign * x, 0, 0) * (speed * Time.deltaTime);
            _rightShip.transform.position += new Vector3(sign * -x, 0, 0) * (speed * Time.deltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                x = context.ReadValue<Vector2>().normalized.x;
            }
            
            if(context.canceled)
            {
                x = 0f;
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            Debug.Log("Fire");
            if (!fireCooldown)
            {
                var position = (leftShipActiveControl) ? _leftShip.transform.position : _rightShip.transform.position;
                Instantiate(boltPrefab, position, Quaternion.identity);
                Debug.Log("Fire");
                fireCooldown = true;
                Invoke(nameof(FireCooldown), fireCoolDownTimer);
            }
        }
        
        public void OnActivateShip(InputAction.CallbackContext context)
        {
            Debug.Log("Activate Ship");
            if (!actionCooldown)
            {
                Debug.Log("Activate Ship");
                leftShipActiveControl = !leftShipActiveControl;
                sign = (leftShipActiveControl) ? 1 : -1;
                actionCooldown = true;
                Invoke(nameof(ActionCooldown), actionCoolDownTimer);
            }
        }

        private void ActionCooldown()
        {
            Debug.Log("Action Cooldown");
            actionCooldown = false;
        }

        private void FireCooldown()
        {
            fireCooldown = false;
        }

    }
}