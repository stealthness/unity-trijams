using System;
using UnityEngine;

namespace _Scripts
{
    public class Door : MonoBehaviour
    {

        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        private DoorAnimator _doorAnimator;

        public Sprite openSprite;
        public Sprite closeSprite;
       [SerializeField] private float delayedDoorClosing = 3f;


        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _doorAnimator = GetComponent<DoorAnimator>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = closeSprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _doorAnimator.ShowOpenDoorAnimation();
                CancelInvoke(nameof(DelayedClosing));
                _spriteRenderer.sprite = openSprite;
            }
            
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _doorAnimator.ShowClosedDoorAnimation();
                Invoke(nameof(DelayedClosing), delayedDoorClosing);
                
            }
        }

        private void DelayedClosing()
        {
            _spriteRenderer.sprite = closeSprite;
        }
    }
}
