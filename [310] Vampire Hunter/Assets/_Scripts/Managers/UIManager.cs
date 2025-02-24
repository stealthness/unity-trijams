using UnityEngine;

namespace _Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
     
        public GameObject gameOverPanel;
        public GameObject gameStartPanel;

        public GameObject player;
        public AudioSource audioSource;
        
        private void Start()
        {
            gameOverPanel.SetActive(false);
            gameStartPanel.SetActive(true);
        }
        
        
        public void GameStart()
        {
            gameOverPanel.SetActive(false);
            gameStartPanel.SetActive(false);
            player.SetActive(true);
            
        }
        
        
        public void GamaOver()
        {
            audioSource.Play();
            gameOverPanel.SetActive(true);
            gameStartPanel.SetActive(false);
            player.SetActive(false);
        }
    }
}