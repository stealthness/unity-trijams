using System;
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
            _animator.Play("Idle");
        }
        
        
        public void PLayAnimation(string animationName)
        {
            _animator.Play(animationName);
        }
    }
}