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
        private float _spawnRate = 0.2f;
        
        
        public GameObject ghoulPrefab;
        
        private void Start()
        {
            leftDoor.Close();
            rightDoor.Close();
            InvokeRepeating(nameof(GenerateGhoul), 2, 1f);
        }
        
        
        private void GenerateGhoul()
        {

            var door = leftDoor;
            
            if (Random.value > _spawnRate)
            {
                _spawnRate += 0.01f;
                return;
            }
            if (Random.value > 0.5f)
            {
                 door = rightDoor;
            }
            
            door.Open();
            var ghoul = Instantiate(ghoulPrefab, door.transform.position, Quaternion.identity);
            ghoul.GetComponent<Ghoul>().target = FindFirstObjectByType<PlayerController>().transform;
            Invoke(nameof(CloseDoor), 2f);
        }
        
        private void CloseDoor()
        {
            leftDoor.Close();
            rightDoor.Close();
        }
    }
}