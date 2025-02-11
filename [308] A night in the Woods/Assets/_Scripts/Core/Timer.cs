
using System;
using TMPro;
using UnityEngine;

namespace _Scripts.Core
{
    public class Timer : MonoBehaviour
    {
        private float _time;
        public TextMeshProUGUI timerText;


        
        private void UpdateTimer()
        {
            _time += 1;
            TimeSpan timeSpan = TimeSpan.FromSeconds(_time);
            timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
        
        public void StopTimer()
        {
            CancelInvoke(nameof(UpdateTimer));
        }

        public void StartTimer()
        {
            _time = 0;
            InvokeRepeating(nameof(UpdateTimer), 1, 1);
        }
    }
}