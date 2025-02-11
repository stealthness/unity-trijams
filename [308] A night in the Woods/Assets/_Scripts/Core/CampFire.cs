using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Scripts.Core
{
    public class CampFire : MonoBehaviour
    {
        private Light2D _light;
        private Animator _animator;
        public AnimationClip[] clips;
        private CampFireData _campFireData;
        private CampFireState _campFireState = CampFireState.Brightly;
        private int firePower = 100;
        
        
        
        private void Awake()
        {
            _light = GetComponentInChildren<Light2D>();
            _animator = GetComponent<Animator>();
        }
        
        private void Start()
        {
            _light.intensity = 1;
            _animator.Play(clips[0].name);
            _campFireData = CampFireData.ReturnData(_campFireState);
            SetCampFireLight();
            StartCoroutine(nameof(CampFireRoutine));
        }
        
        
        private void DecreaseFirePower()
        {
            firePower--;
            if (firePower <= 0)
            {
                _campFireState = _campFireData.DecreaseState(_campFireState);
                _campFireData = CampFireData.ReturnData(_campFireState);
                _animator.Play(clips[2].name);
                SetCampFireLight();
            }

            if (firePower >= 100)
            {
                _campFireState = CampFireState.Brightly;
                _campFireData = CampFireData.ReturnData(_campFireState);
            }
        }
        

        private IEnumerator CampFireRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(20);
            while (true)
            {
                yield return wait;
                _campFireState = _campFireData.DecreaseState(_campFireState);
                _campFireData = CampFireData.ReturnData(_campFireState);
                SetCampFireLight();
            }
        }

        private void SetCampFireLight()
        {
            _light.intensity = _campFireData.lightIntensity;
            _light.pointLightInnerRadius = _campFireData.lightInnerRadius;
            _light.pointLightOuterRadius = _campFireData.lightOuterRadius;
        }

        class CampFireData
        {
            internal float lightIntensity;
            internal float lightInnerRadius;
            internal float lightOuterRadius;

            public CampFireData()
            {
                lightIntensity = 0;
                lightInnerRadius = 0;
                lightOuterRadius = 0;
            }

            internal CampFireData(float lightIntensity, float lightInnerRadius, float lightOuterRadius)
            {
                this.lightIntensity = lightIntensity;
                this.lightInnerRadius = lightInnerRadius;
                this.lightOuterRadius = lightOuterRadius;
            }


            internal static CampFireData ReturnData(CampFireState state)
            {
                return state switch
                {
                    CampFireState.Brightly => new CampFireData(1.2f, 12, 20),
                    CampFireState.Burning => new CampFireData(1, 8, 12),
                    CampFireState.Embers => new CampFireData(0.5f, 4, 8),
                    CampFireState.Extinguished => new CampFireData(0.1f, 2 , 8),
                    _ => new CampFireData()
                };
            }

            internal CampFireState DecreaseState(CampFireState state)
            {
                return state switch
                {
                    CampFireState.Brightly => CampFireState.Burning,
                    CampFireState.Burning => CampFireState.Embers,
                    CampFireState.Embers => CampFireState.Extinguished,
                    CampFireState.Extinguished => CampFireState.Extinguished,
                    _ => CampFireState.Extinguished
                };
            }

            internal CampFireState IncreaseState(CampFireState state)
            {
                return state switch
                {
                    CampFireState.Brightly => CampFireState.Brightly,
                    CampFireState.Burning => CampFireState.Brightly,
                    CampFireState.Embers => CampFireState.Burning,
                    CampFireState.Extinguished => CampFireState.Embers,
                    _ => CampFireState.Extinguished
                };
            }
        }

        internal enum CampFireState
        {
            Brightly,
            Burning,
            Embers,
            Extinguished
            
            
        }

        public void AddStick()
        {
            _campFireState = _campFireData.IncreaseState(_campFireState);
        }
    }
}