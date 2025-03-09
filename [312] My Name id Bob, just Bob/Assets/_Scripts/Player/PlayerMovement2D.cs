using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement2D : Movement2D
    {
        
        private const float Gravity = 9.81f;
        private bool _isFalling = false;
        private float _fallSpeed = 5;


        protected internal override void OnMove(Vector2 direction)
        {
            if (_isFalling)
            {
                return;
            }
            base.OnMove(direction);
        }


        public void OnFall()
        {
            Debug.Log("PlayerMovement2D::Falling");
            _isFalling = true;
            _fallSpeed += Gravity * Time.deltaTime;
            speed = _fallSpeed;
            _dir = new Vector2(0, -1);
        }
    }
}
