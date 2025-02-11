using System;
using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// This class is responsible for the movement of the Player or NPC  in a top-down 2D game.
    /// </summary>
    public abstract class Movement2DTopdown : MonoBehaviour
    {
        
        protected bool Moving = true;
        protected SpriteRenderer SpriteRenderer;
        protected Vector3 Direction;


        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Direction = Vector2.up;
        }


        private void Update()
        {
            if (!Moving) return;
            
            Move();
            CheckDirection();
        }

        private void CheckDirection()
        {
            SpriteRenderer.flipX = Direction.x > 0;
        }

        protected abstract void Move();
        
        

    }
}