using UnityEngine;

namespace _Scripts.UI
{
    public class CardPanel : MonoBehaviour
    {
        public GameObject cardPanel;
        
        
        public void ShowCardPanel()
        {
            Debug.Log("Show Card Panel");
            cardPanel.SetActive(true);
            
        }
        
        
        public void SelectCard(int cardIndex)
        {
            Debug.Log("Selected card: " + cardIndex);
        }
        
        
        
    }
}