using UnityEngine;

namespace _Scripts
{
    class DoorAnimator: MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ShowOpenDoorAnimation()
        {
            _animator.Play("Open Door");
            
        }

        public void ShowClosedDoorAnimation()
        {
            _animator.Play("Close Door");
        }
        
    }
}