using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Managers
{
    public class VampireManager : MonoBehaviour
    {
        
        public Transform[] _startLocations;
        public GameObject vampPrefab;
        public float _vampSpawnRate = 0.2f;
        
        private void Awake()
        {
            if (_startLocations.Length == 0)
            {
                Debug.LogError("No start locations set for the vampire");
            }
        }
        
        private void Start()
        {
            InvokeRepeating(nameof(GenerateVampire), 0, 0.5f);
        }


        private void GenerateVampire()
        {
            
            if (UnityEngine.Random.value > _vampSpawnRate)
            {
                _vampSpawnRate += 0.01f;
                return;
            }
            var randomIndex = UnityEngine.Random.Range(0, _startLocations.Length);
            var randomStartLocation = _startLocations[randomIndex];
            var vamp = Instantiate(vampPrefab, randomStartLocation.position, Quaternion.identity);
        }
    }
}