using _Scripts.Core;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GhoulManager : MonoBehaviour
    {
        
        
        [field: SerializeField] public Door leftDoor;
        [field: SerializeField] public Door rightDoor;
        private float _spawnRate = 0.2f;
        private Door _door;
        
        
        public GameObject ghoulPrefab;
        
        private void Start()
        {
            leftDoor.Close();
            rightDoor.Close();
            InvokeRepeating(nameof(GenerateGhoulEvent), 2, 3f);
        }
        
        
        private void GenerateGhoulEvent()
        {

            if (Random.value > _spawnRate)
            {
                return;
            }
            
            _door = GetDoor();
            _door.Open();
            _spawnRate += 0.01f;
            Invoke(nameof(DelayedGhoulSpawn), 1f);

        }

        private Door GetDoor()
        {
            return (Random.value > 0.5f) ? leftDoor : rightDoor;
        }

        private void DelayedGhoulSpawn()
        {
                var ghoul = Instantiate(ghoulPrefab, _door.transform.position, Quaternion.identity);
                Invoke(nameof(CloseDoor), 1f); 
        }
        
        private void CloseDoor()
        {
            _door.Close();
        }
    }
}