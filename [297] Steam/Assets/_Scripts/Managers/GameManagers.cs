using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Scripts.Managers
{
    /// <summary>
    /// GameManager is a singleton class that manages the game state.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private bool startMenuAtStart = false;
        [SerializeField] private bool musicOnAtStart = true;
        [SerializeField] private float timedDelayedShowMenuUI = 3f;
        private AudioSource _audioSource;


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
            Debug.Log("GameManager is ready");
            if (startMenuAtStart)
            {
                Time.timeScale = 0;
                StartMenuManager.Instance.ShowStartMenu();
            }
            else
            {
                Time.timeScale = 1;
                StartMenuManager.Instance.ShowGameStart();
                StartGame();
            }
            ToggleMusic(musicOnAtStart);
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

        /// <summary>
        /// Toggle the game's music on or off.
        /// </summary>
        /// <param name="musicOn"></param>
        private void ToggleMusic(bool musicOn)
        {
            _audioSource.enabled = musicOn;
            if (musicOn)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }

        /// <summary>
        /// Restart the game using the scene manager.
        /// </summary>
        public void RestartGame()
        {
            Debug.Log("Restart Game");
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// SHows the game over screen after delay.
        /// </summary>
        public void GameOver()
        {
            Debug.Log("Game Over");
            Invoke(nameof(DelayedShowGameOverMenuUI), timedDelayedShowMenuUI);
        }

        
        private void DelayedShowGameOverMenuUI()
        {
            StartMenuManager.Instance.ShowGameOver();
            Time.timeScale = 0;
        }

        /// <summary>
        /// Starts a game
        /// </summary>
        public void StartGame()
        {
            Time.timeScale = 1;
            Debug.Log("Start Game");
        }

        /// <summary>
        /// Level complete, show the level complete screen after a delay
        /// </summary>
        public void LevelComplete()
        {
            Debug.Log("Level Complete");
            Invoke(nameof(DelayedLevelCompleteExit), timedDelayedShowMenuUI);
        }


        /// <summary>
        ///     Show the level complete screen after a delay.
        /// </summary>
        private void DelayedLevelCompleteExit()
        {
            StartMenuManager.Instance.ShowLevelComplete();
            Time.timeScale = 0;
        }
    }
}
