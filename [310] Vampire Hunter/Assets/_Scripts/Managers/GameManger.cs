using _Scripts.Player;
using TMPro;
using UnityEngine;
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
            _score = 0;
        }
 

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }    
        }
    
    
        public void StartGame()
        {
            Time.timeScale = 1;
            
        }
        
        public void GameOver()
        {
            Time.timeScale = 0;
            _audioSource.Play();
        }
        
        public void UpdateScore(int score)
        {
            _score += score;
            scoreText.text = "Kill Count : " +_score;
        }
        
    }
}
