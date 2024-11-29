using UnityEngine;

namespace _Scripts.Player
{
    [CreateAssetMenu]
    public class PlayerMovement2DData : Movement2DData
    {
        [Header("No. Jumps")] [Tooltip("The amount of jumps the player can do, before a ground check")]
        public int maxJumps = 1;
        
        [Header("Player Layer")] [Tooltip("The layer that the player is on")]
        public LayerMask playerLayer;
    }
}