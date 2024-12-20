using UnityEngine;

namespace _Scripts.Player
{
    public class Movement2DData : ScriptableObject
    {
        [Header("Objects Speed")] [Tooltip("The speed of the object")]
        public float speed = 5f;
        [Header("Jump Force")] [Tooltip("The force of the jump")]
        public float jumpForce = 5f;
    }
}