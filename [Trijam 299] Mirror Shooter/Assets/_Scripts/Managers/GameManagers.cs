using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;
        
        [SerializeField] private bool _startMenuAtStart = false;


        private void Awake()
        {
            if (GameManager.Instance == null)
            {
                GameManager.Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }


        void Start()
        {
            Debug.Log("GameManager is ready");
            if (_startMenuAtStart)
            {
                Time.timeScale = 0;
                StartMenuManager.Instance.ShowStartMenu();
            }
            else
            {
                StartGame();
            }
        }
        
        public void RestartGame()
        {
            Time.timeScale = 1;
            Debug.Log("Restart Game");
        }
        
        
        public void GameOver()
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
            Debug.Log("Start Game");
        }

    }
}
