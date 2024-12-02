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
        [SerializeField] private bool musicOnAtStart = true;
        AudioSource _audioSource;


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
            _audioSource = GetComponent<AudioSource>();
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
                StartMenuManager.Instance.ShowGameStart();
            }
            Debug.Log("GameManager is ready");
            ToggleMusic(musicOnAtStart);
        }

        private void ToggleMusic(bool musicOn)
        {
            if (musicOn)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMusic(!_audioSource.isPlaying);
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
