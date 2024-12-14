using UnityEngine;

namespace _Scripts.Player
{
    [CreateAssetMenu]
    public class PlayerMovement2DData : ScriptableObject
    {
        
        [Header("Objects Speed")] [Tooltip("The speed of the object")]
        public float speed = 5f;
        [Header("Jump Force")] [Tooltip("The force of the jump")]
        public float jumpForce = 5f;
        
        [Header("No. Jumps")] [Tooltip("The amount of jumps the player can do, before a ground check")]
        public int maxJumps = 1;
        
        [Header("Player Layer")] [Tooltip("The layer that the player is on")]
        public LayerMask playerLayer;
    }
}