using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (_startMenuAtStart)
            {
                Time.timeScale = 0;
                StartMenuManager.Instance.ShowStartMenu();
            }
            else
            {
                Time.timeScale = 1;
            }
            Debug.Log("GameManager is ready");
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
        
        
        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
            Debug.Log("Restart Game");
        }
        
        
        public void GameOver()
        {
            Debug.Log("Game Over");
            Invoke(nameof(DelayedExit), 3f);
        }
        
        private void DelayedExit()
        {
            StartMenuManager.Instance.ShowGameOver();
            Time.timeScale = 0;
        }

        public void StartGAme()
        {
            Time.timeScale = 1;
            Debug.Log("Start Game");
        }

        public void LevelComplete()
        {
            Debug.Log("Level Complete");
            Invoke(nameof(DelayedLevelExit), 3f);
        }
        
        
        private void DelayedLevelExit()
        {
            StartMenuManager.Instance.ShowLevelComplete();
            Time.timeScale = 0;
        }
    }
}
