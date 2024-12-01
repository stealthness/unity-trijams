using System;
using UnityEngine;

namespace _Scripts.Managers
{
    /// <summary>
    /// This is a generic GameManager class that can be used in any game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;
        
        [SerializeField] private bool activateStartMenuOnStart = true;
        private GameState gameState;

        private void Awake()
        {
            if(GameManager.Instance == null)
            {
                GameManager.Instance = this;
            }
            else
            {
                Destroy(gameObject);
                DontDestroyOnLoad(this);
            }
        }

        /// <summary>
        /// Start is called before the first frame update, freezes the game if activateStartMenuOnStart is false
        /// </summary>
        void Start()
        {
            Debug.Log("GameManager is starting");
            if (!activateStartMenuOnStart)
            {
                gameState = GameState.Playing;
                StartGame();
            }else
            {
                Time.timeScale = 0;
                gameState = GameState.StartMenu;
            }
        }


        private void Update()
        {
            switch (gameState)
            {
                case GameState.StartMenu:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        gameState = GameState.Playing;
                        StartGame();
                    }
                    break;
                case GameState.Playing:
                case GameState.Paused:
                    CheckPause();
                    CheckRestart();
                    break;
                case GameState.GameOver:
                    CheckRestart();
                    break;
                        
            }
        }
        
        private void CheckPause()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnTogglePause(true);
            }
        }
        
        private void CheckRestart()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnRestartGame();
            }
        }
        

        /// <summary>
        /// Starts the game
        /// </summary>
        private void StartGame()
        {
            Time.timeScale = 1;
        }
        
        /// <summary>
        /// OnRestartGame is called when the player wants to restart the game
        /// </summary>
        public void OnRestartGame()
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        
        
        void OnTogglePause(bool pause)
        {
            if (gameState == GameState.Playing && pause)
            {
                Time.timeScale = 0;
                GameState gameState = GameState.Paused;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void GameOver(string steamPressureTooHigh)
        {
            Debug.Log("Game Over: " + steamPressureTooHigh);
            gameState = GameState.GameOver;
            Time.timeScale = 0;
        }
    }

    enum GameState
    {
        StartMenu,
        Playing,
        Paused,
        GameOver
    }
}
