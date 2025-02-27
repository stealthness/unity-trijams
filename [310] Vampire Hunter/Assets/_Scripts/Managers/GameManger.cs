using _Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManger : MonoBehaviour
    {
        
        private AudioSource _audioSource;
        
        public static GameManger Instance;
        
        public TextMeshProUGUI scoreText;

        private PlayerController _player;
        private int _score;

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
            
            _audioSource = GetComponent<AudioSource>();
        }


        private void Start()
        {
            Time.timeScale = 0;
            ResetScore();
        }


        public void ResetGame(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SceneManager.LoadScene(0);
            }
        }
    
    
        /// <summary>
        /// Starts the game by setting the time scale to 1 and resetting the score
        /// </summary>
        public void StartGame()
        {
            Time.timeScale = 1;
            ResetScore();
            
        }
        
        /// <summary>
        /// Triggers the Game Over State and plays the laughing game over sound and stops the game time
        /// </summary>
        public void GameOver()
        {
            Time.timeScale = 0;
            _audioSource.Play();
        }

        /// <summary>
        /// Resets the Score to 0, and Updates the score text
        /// </summary>
        private void ResetScore()
        {
            _score = 0;
            UpdateScore(0);
        }
        
        /// <summary>
        /// Updates the score by adding the score to the current score
        /// </summary>
        /// <param name="score"></param>
        public void UpdateScore(int score)
        {
            _score += score;
            scoreText.text = "Kill Count : " + _score;
        }
        
    }
}
