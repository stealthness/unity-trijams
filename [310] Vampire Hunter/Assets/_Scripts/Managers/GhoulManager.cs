using _Scripts.Core;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GhoulManager : MonoBehaviour
    {
        
        [field: SerializeField] public Transform[] _startLocations;
        [field: SerializeField] public Door leftDoor;
        [field: SerializeField] public Door rightDoor;
        
        
        public GameObject ghoulPrefab;
        
        private void Start()
        {
            leftDoor.Close();
            rightDoor.Close();
            InvokeRepeating(nameof(GenerateGhoul), 0, 10f);
        }
        
        
        private void GenerateGhoul()
        {
           leftDoor.Open();
            var ghoul = Instantiate(ghoulPrefab, _startLocations[0].position, Quaternion.identity);
            ghoul.GetComponent<Ghoul>().target = FindFirstObjectByType<PlayerController>().transform;
            Invoke(nameof(CloseDoor), 2f);
        }
        
        private void CloseDoor()
        {
            leftDoor.Close();
        }
    }
}