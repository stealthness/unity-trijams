using System;
using UnityEngine;

namespace _Scripts.Core
{
    public class Movement2D4 : MonoBehaviour
    {
        public void Move(Dir2D4 dir)
        {
            transform.position += dir switch
            {
                Dir2D4.Up => Vector3.up,
                Dir2D4.Down => Vector3.down,
                Dir2D4.Left => Vector3.left,
                Dir2D4.Right => Vector3.right,
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }
    }


    public enum Dir2D4
    {
        Up,
        Down,
        Left,
        Right
    }
}