using System;
using UnityEngine;

namespace _Scripts
{
    
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

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


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Start GameManager");
        }


        public void StartGame()
        {
            
        }
    }
}
