using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Spiders
{
    /// <summary>
    /// The SpiderManager class is responsible for spawning spiders at random spawn points
    /// </summary>
    public class SpiderManager : MonoBehaviour
    {
        public static SpiderManager Instance;
        
        
        public GameObject spiderPrefab;
        public Transform[] spawnPoints;
        private float _spawnInterval = 10f;
        private Coroutine _spawnCoroutine;
        private const float MinSpawnInterval = 2f;
        private const float DecreaseInterval = 1f;
        private bool _spawn = true;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogWarning("No spawn points found");
            }
            _spawnCoroutine = StartCoroutine(SpawnSpiderRoutine());
        }
        
        /// <summary>
        /// Spawns a spider every _spawnInterval which decreases over time and stops when game is over
        /// </summary>
        private IEnumerator SpawnSpiderRoutine()
        {
            while (true)
            {
                SpawnSpider();
                yield return new WaitForSeconds(_spawnInterval);
                if (_spawnInterval > MinSpawnInterval)
                {
                    _spawnInterval -= DecreaseInterval;
                }
            }
        } // infinite loop is intentional here


        /// <summary>
        /// Spawns a spider at a random spawn point
        /// </summary>
        private void SpawnSpider()
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogWarning("No spawn points found");
                return;
            }
            var randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            var spider = Instantiate(spiderPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            spider.GetComponent<SpiderMovement2DTopdown>().SetTarget(GameObject.FindWithTag("Player"));
            
        }

        public void StopAllSpiders()
        {
            Debug.Log("Stopping all spiders");
            StopSpawning();
            var spiders = GameObject.FindGameObjectsWithTag("Spider");
            foreach (var spider in spiders)
            {
                spider.GetComponent<SpiderMovement2DTopdown>().Stop();
            }
        }

        private void StopSpawning()
        {
            Debug.Log("Stop Spawning");
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
                _spawn = false;
            }
        }
    }
}