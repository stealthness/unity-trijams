using System.Collections;
using TMPro;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Scripts.Core
{
    public class CampFire : MonoBehaviour
    {
        private Light2D _light;
        private Animator _animator;
        private int firePower = 100;
        
        
        public AnimationClip[] clips;
        public TextMeshProUGUI firePowerText;    
        
        private void Awake()
        {
            _light = GetComponentInChildren<Light2D>();
            _animator = GetComponent<Animator>();
        }
        
        private void Start()
        {
            Debug.Log("CampFire Started");
            _light.intensity = 1;
            _animator.Play(clips[0].name);
            InvokeRepeating(nameof(DecreaseFirePower), 1, 1);
        }

        private void Update()
        {
            firePowerText.text = $"{firePower}";
            if (firePower > 90)
            {
                _animator.Play(clips[0].name);
            } else if (firePower > 70)
            {
                _animator.Play(clips[1].name);
            } else if (firePower > 50)
            {
                _animator.Play(clips[2].name);
            } else if (firePower > 20)
            {
                _animator.Play(clips[3].name);
            }
        }
        
        
        private void DecreaseFirePower()
        {
            firePower = Mathf.Max(0, firePower - 1);
            if (firePower <= 0)
            {
                
                _light.intensity = 0;
                _animator.Play(clips[1].name);
                CancelInvoke(nameof(DecreaseFirePower));
            }
            _light.pointLightInnerRadius = 4 * (firePower / 100f) + 1;
            _light.pointLightOuterRadius = 8 * (firePower / 100f) + 3;
            _light.intensity = firePower / 100f;
            firePowerText.text = $"{firePower}";
            
            

        }
        
        public void AddStick(int sticks)
        {
            firePower = Mathf.Min(100, firePower + sticks);
        }
        


    }
}