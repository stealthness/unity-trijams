using System;
using _Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    /// <summary>
    /// The GameManager class is responsible for managing the game state.
    ///
    /// Additional information:
    /// Spider Sprites from Elthen's Pixel Art Shop from Itich.io https://elthen.itch.io/2d-pixel-art-spider-sprites
    /// camp fire from LadySachmet from Itich.io https://ladysachmet.itch.io/animated-campfire-36px
    /// music Forest river by Oleksii_Kalyna https://pixabay.com/music/modern-classical-forest-river-240225/ (content ID)
    /// font bridgnorth from https://www.1001freefonts.com/bridgnorth.font
    /// kick sound - pixabay - https://pixabay.com/sound-effects/kick-183936/
    /// scream sound - pixabay - https://pixabay.com/sound-effects/scream-85566/
    /// snap https://pixabay.com/sound-effects/twig-snap-classic-85662/
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        
        private GameObject _player;
        
        public Timer Timer;
        public GameObject startPanel;
        public GameObject gameOverPanel;
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
            _player = GameObject.FindWithTag("Player");
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _player.SetActive(false);
            Debug.Log("Game Started");
            Time.timeScale = 0;
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        public void StartGame()
        {
            Time.timeScale = 1;
            Timer.StartTimer();
            startPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            _player.SetActive(true);
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
            gameOverPanel.SetActive(false);
        }
        
        
        public void GameOver()
        {
            Debug.Log("Game Over");
           Timer.StopTimer();
           Invoke(nameof(DelayedGameOver), 3f);
        }
        
        
        private void DelayedGameOver()
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
 
    }
}
