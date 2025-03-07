using System;
using UnityEngine;

namespace _Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance { get; private set; }

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
            Debug.Log("Game Manager Start");
        }

    }

}

