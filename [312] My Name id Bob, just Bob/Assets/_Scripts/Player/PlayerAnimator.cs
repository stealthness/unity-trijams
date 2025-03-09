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


        private void Start()
        {
            _animator.Play(AnimationStrings.Walk);
        }
        
        
        public void PlayAnimation(string animationName)
        {
            _animator.Play(animationName);
        }

        public float GetAnimationLength(string animationName)
        {
            var clip = _animator.runtimeAnimatorController.animationClips;
            foreach (var animationClip in clip)
            {
                if (animationClip.name == animationName)
                {
                    return animationClip.length;
                }
            }

            return 0;
        }
    }
}