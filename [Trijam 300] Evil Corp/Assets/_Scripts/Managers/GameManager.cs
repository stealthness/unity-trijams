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

        private void Update()
        {
            if (gameState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
            {
                OnRestartGame();
            } 
            if (gameState == GameState.Playing && Input.GetKeyDown(KeyCode.Escape))
            {
                OnTogglePause(true);
            } 
            if (gameState == GameState.Paused && Input.GetKeyDown(KeyCode.Escape))
            {
                OnTogglePause(false);
            }
        }

        /// <summary>
        /// Start is called before the first frame update, freezes the game if activateStartMenuOnStart is false
        /// </summary>
        void Start()
        {
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
                gameState = GameState.Paused;
            }
            else
            {
                gameState = GameState.Playing;
                Time.timeScale = 1;
            }
        }
        
        public void OnGameOver()
        {
            Time.timeScale = 0;
            gameState = GameState.GameOver;
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
