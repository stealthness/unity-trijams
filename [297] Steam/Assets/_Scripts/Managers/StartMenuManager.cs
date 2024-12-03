using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Managers
{
    public class StartMenuManager : MonoBehaviour
    {
        
        public static StartMenuManager Instance;
        public GameObject startMenu;
        public GameObject levelComplete;
        public GameObject gameOverPanel;
        public GameObject gameUI;
        public GameObject MessagePanel;
        public TextMeshProUGUI _messageTextBox;
        [SerializeField] private float messageHideDelay = 5f;

        private void Awake()
        {
            if (StartMenuManager.Instance == null)
            {
                StartMenuManager.Instance = this;
                
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        
        void Start()
        {
            Debug.Log("Start Menu Manager is ready");
        }


        public void ShowGameStart()
        {
            startMenu.SetActive(false);
            gameUI.SetActive(true);
            ShowMessage("Fix at least 3 engine to exit");
        }

        public void ShowStartMenu()
        {
            Debug.Log("Show Start Menu");
            startMenu.SetActive(true);
            gameUI.SetActive(false);
            
        }
        
        public void HideStartMenu()
        {
            Debug.Log("Hide Start Menu");
            startMenu.SetActive(false);
        }

        public void ShowLevelComplete()
        {
            levelComplete.SetActive(true);
        }

        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
        }

        public void ShowMessage(string message)
        {
            if (MessagePanel.activeSelf)
            {
                CancelInvoke();
            }
            else
            {
                MessagePanel.SetActive(true);
            }
            _messageTextBox.text = message;
            Invoke(nameof(HideMessage), messageHideDelay);
        }
        
        public void HideMessage()
        {
            MessagePanel.SetActive(false);
            _messageTextBox.text = "";
        }
    }
}