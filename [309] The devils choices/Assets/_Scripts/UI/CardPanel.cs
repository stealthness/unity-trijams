using UnityEngine;

namespace _Scripts.UI
{
    public class CardPanel : MonoBehaviour
    {
        public GameObject cardPanel;
        public GameObject unsuccesfulPanel;
        public GameObject succesfulPanel;
        
        
        public void ShowCardPanel()
        {
            Debug.Log("Show Card Panel");
            cardPanel.SetActive(true);
            
        }
        
        
        public void SelectCard(int cardIndex)
        {
            Debug.Log("Selected card: " + cardIndex);
            switch (cardIndex)
            {
                case 1:
                    ShowSuccessfulPanel();
                    break;
                default:
                    ShowUnsuccesfulPanel();
                    break;
                
            }

        }

        private void ShowSuccessfulPanel()
        {
            cardPanel.SetActive(false);
            succesfulPanel.SetActive(true);
        }

        private void ShowUnsuccesfulPanel()
        {
            cardPanel.SetActive(false);
            unsuccesfulPanel.SetActive(true);
        }


        public void CloseCardPanel()
        {
            Debug.Log("Close Card Panel");
            cardPanel.SetActive(false);
            unsuccesfulPanel.SetActive(false);
            succesfulPanel.SetActive(false);
        }
        
        
        
    }
}