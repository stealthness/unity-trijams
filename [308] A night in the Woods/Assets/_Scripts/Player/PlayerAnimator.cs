using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayAnimation(PlayerAnimation anim)
        {
            switch (anim)
            {
                case PlayerAnimation.Moving:
                    _animator.Play("Moving");
                    break;
                case PlayerAnimation.Idle:
                    _animator.Play("Idle");
                    break;
                case PlayerAnimation.Dead:
                    _animator.Play("Dead");
                    break;
                case PlayerAnimation.Kick:
                    _animator.Play("Kick");
                    break;
                default:
                    _animator.Play("Idle");
                    break;

            }
        }

        public float GetClipLength(PlayerAnimation clip)
        { 
            return _animator.runtimeAnimatorController.animationClips[(int)clip].length;
        }
    }

    public enum PlayerAnimation
    {
        Idle,
        Moving,
        Dead,
        Kick
        
        /*,
        Jump,
        Fall,
        Attack,
        Dead*/
    }
}
