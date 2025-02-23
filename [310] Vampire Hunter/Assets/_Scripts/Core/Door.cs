using UnityEngine;

namespace _Scripts.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Door : MonoBehaviour
    {
        
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Sprite openSprite;
        [SerializeField] private Sprite closedSprite;
        
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Close()
        {
            _spriteRenderer.sprite = closedSprite;
            Debug.Log("Door closed");
        }
        
        public void Open()
        {
            _spriteRenderer.sprite = openSprite;
            Debug.Log("Door opened");
            
        }
        
    }
}