using System;
using UnityEngine;

namespace _Scripts.Core
{
    

 
    [RequireComponent(typeof(Collider2D))]
    public abstract class Movement2D : MonoBehaviour
    {

       


        [SerializeField] protected float speed = 5f;
        private Vector2 _dir;


        private void Update()
        {
            transform.Translate(_dir  * (speed * Time.deltaTime));
        }

        protected internal virtual void OnMove(Vector2 direction)
        {
            _dir = direction;
        }
    }
}
