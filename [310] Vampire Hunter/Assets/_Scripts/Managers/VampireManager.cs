using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Managers
{
    public class VampireManager : MonoBehaviour
    {
        
        public Transform[] _startLocations;
        public GameObject vampPrefab;
        public float _vampSpawnRate = 0.1f;
        private float _vampSpawnRateIncrease = 0.0005f;

        private void Awake()
        {
            if (_startLocations.Length == 0)
            {
                Debug.LogError("No start locations set for the vampire");
            }
        }
        
        private void Start()
        {
            InvokeRepeating(nameof(GenerateVampire), 2, 0.5f);
        }


        private void GenerateVampire()
        {
            
            if (UnityEngine.Random.value > _vampSpawnRate)
            {
                _vampSpawnRateIncrease = 0.01f;
                _vampSpawnRate += _vampSpawnRateIncrease;
                return;
            }
            var randomIndex = UnityEngine.Random.Range(0, _startLocations.Length);
            var randomStartLocation = _startLocations[randomIndex];
            var vamp = Instantiate(vampPrefab, randomStartLocation.position, Quaternion.identity);
        }
    }
}