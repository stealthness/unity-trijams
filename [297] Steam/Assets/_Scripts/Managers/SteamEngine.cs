using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers
{
    public class SteamEngine : MonoBehaviour
    {

        public Slider engineSlider;
        public Sprite engineOnSprite;
        public Sprite engineOffSprite;
        private int _engineHealth = 50;
        private int _maxEngineHealth = 100;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = engineOnSprite;
            engineSlider.value = _engineHealth;
            InvokeRepeating(nameof(CheckEngine), 1f, 1f);    
        }

        private void CheckEngine()
        {
            if (_engineHealth <= 0 || _engineHealth >= _maxEngineHealth)
            {
                Debug.Log("Engine is broken : " + _engineHealth);
                _spriteRenderer.sprite = engineOffSprite;
                CancelInvoke(nameof(CheckEngine));
                GameManager.Instance.GameOver();
            }
            else
            {
                            _engineHealth += 1;
                            engineSlider.value = _engineHealth;
            }

        }
        
        
    }
}