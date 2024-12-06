using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float x;
        private GameObject _leftShip;
        private GameObject _rightShip;
        [SerializeField] private bool leftShipActiveControl;
        [SerializeField] private int sign = 1;
        
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
                Debug.Log("x: " + x);
            }
            
            if(context.canceled)
            {
                x = 0f;
            }
        }

        public void OnActivateShip(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                leftShipActiveControl = !leftShipActiveControl;
                sign = (leftShipActiveControl) ? 1 : -1;
            }
        }


    }
}