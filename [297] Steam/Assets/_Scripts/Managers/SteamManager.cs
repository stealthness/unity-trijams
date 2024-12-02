using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers
{
    public class SteamManager :MonoBehaviour
    {
        public static SteamManager Instance;
        
        
        public Slider engineSlider;
        private int _engineHealth = 100;
        private int _maxEngineHealth = 100;
        [SerializeField] private int _pressureLossPerTick = 3;
        [SerializeField]  int _tickRate = 1;
        
        
        

        private void Awake()
        {
            if (SteamManager.Instance == null)
            {
                SteamManager.Instance = this;
                
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            engineSlider.value = _engineHealth;
            InvokeRepeating(nameof(CheckEngine), 1f, _tickRate);    
            
        }

        public void AddPressure(int amount)
        {
            _engineHealth = Mathf.Min(_maxEngineHealth, _engineHealth + amount);
        }
        
        
        private void CheckEngine()
        {
            if (_engineHealth <= 0 )
            {
                Debug.Log("Engine is broken : " + _engineHealth);
                CancelInvoke(nameof(CheckEngine));
                GameManager.Instance.GameOver();
            }
            else
            {
                _engineHealth -= _pressureLossPerTick;
                engineSlider.value = _engineHealth;
            }

        }
    }
}