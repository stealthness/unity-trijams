using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Scripts.Managers
{
    public class SteamEngin : MonoBehaviour
    {
        public TextMeshProUGUI steamPressureText;
        public Slider steamPressureSlider;
        public static SteamEngin Instance;
        private int _steamPressure;

        private void Awake()
        {
            if (SteamEngin.Instance == null)
            {
                SteamEngin.Instance = this;
            }
            else
            {
                Destroy(gameObject);
                DontDestroyOnLoad(this);
            }
        }
    

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _steamPressure = 50;
            steamPressureSlider.value = _steamPressure;
            InvokeRepeating(nameof(AdjustPressure), 1f, 1f);
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        private void AdjustPressure()
        {
            _steamPressure += 1;
            UpdateSteamPressureText();
            CheckPressure();
        }

        private void UpdateSteamPressureText()
        {
            steamPressureText.text = $"Steam Pressure: {_steamPressure}";
            steamPressureSlider.value = _steamPressure;
        }

        private void CheckPressure()
        {
            if (_steamPressure > SteamPressureMax)
            {
                GameManager.Instance.GameOver("Steam pressure too high : " + _steamPressure);
                CancelInvoke();
            }
        }

        private const int SteamPressureMax = 100;
        
    }
}
