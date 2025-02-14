using UnityEngine;

namespace _Scripts.UI
{
    public class StartPanel : MonoBehaviour
    {
        public GameObject startPanel;
        
        
        
        public void OnStartGame()
        {
            Debug.Log("Start Game");
            startPanel.SetActive(false);
            GameManager.Instance.StartGame();
        }
    }
}