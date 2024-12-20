using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Managers
{
    /// <summary>
    /// The start menu UI is attached to the StartMenuUI GameObject in the scene.
    /// It has three buttons: Start, Options, and Credits.
    /// </summary>
    public class StartMenuUI : MonoBehaviour
    {
        public GameObject StartPanel;
        public UnityEvent OnStartGameClicked;
        public UnityEvent OnCreditsClicked;
        public UnityEvent OnOptionsClicked;
        
        /// <summary>
        /// Start the game
        /// </summary>
        public void OnStartGame()
        {
            Debug.Log("Start Game Clicked");
            OnStartGameClicked?.Invoke();
            StartPanel.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Selects the Options menu
        /// </summary>
        public void OnOptions()
        {
            Debug.Log("Options Clicked");
            OnOptionsClicked?.Invoke();
            StartPanel.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Selects the Credits menu
        /// </summary>
        public void OnCredits()
        {
            Debug.Log("Credits Clicked");
            OnCreditsClicked?.Invoke();
            StartPanel.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Show the Start Menu
        /// </summary>
        public void ShowStartMenu()
        {
            Debug.Log("Show Start Menu");
            StartPanel.gameObject.SetActive(true);
        }
        
        
    }
}
