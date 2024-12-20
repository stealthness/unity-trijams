using UnityEngine;

namespace _Scripts.Managers
{
    public class StartMenuManager : MonoBehaviour
    {
        
        public static StartMenuManager Instance;
        public GameObject startMenu;
        
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
            startMenu.SetActive(false);
        }

        public void ShowStartMenu()
        {
            Debug.Log("Show Start Menu");
            startMenu.SetActive(true);
        }
        
        public void HideStartMenu()
        {
            Debug.Log("Hide Start Menu");
            startMenu.SetActive(false);
        }
        
    }
}