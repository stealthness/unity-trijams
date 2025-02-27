using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameStartManager : MonoBehaviour
    {

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name != "ForestScene")
            {
                Debug.LogWarning("Scene is not ForestScene");
            }
        }
        
        private void Start()
        {
            Time.timeScale = 1;
        }
    }
}