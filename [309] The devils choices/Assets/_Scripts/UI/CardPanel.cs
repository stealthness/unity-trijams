using System.Linq;
using _Scripts.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class CardPanel : MonoBehaviour
    {
        public GameObject cardPanel;
        public GameObject unsuccesfulPanel;
        public GameObject succesfulPanel;
        public Card[] cards;
        private Card[] shuffledCards; 
        public GameObject Image1;
        public GameObject Image2;
        public GameObject Image3;


        private void Start()
        {
            ShuffleCards();
        }

        private void ShuffleCards()
        {
            
            System.Random random = new System.Random();
            shuffledCards = cards.OrderBy(x => random.Next()).Take(3).ToArray();

            Image1.GetComponent<Image>().sprite = shuffledCards[0].cardImage;
            shuffledCards[0].DrawCard(Image1, 0);
            Image2.GetComponent<Image>().sprite = shuffledCards[1].cardImage;
            shuffledCards[1].DrawCard(Image2, 1);
            Image3.GetComponent<Image>().sprite = shuffledCards[2].cardImage;
            shuffledCards[2].DrawCard(Image3, 2);
        }


        public void ShowCardPanel()
        {
            Debug.Log("Show Card Panel");
            cardPanel.SetActive(true);
            ShuffleCards();
            
        }
        
        
        
        
        public void SelectCard(int cardIndex)
        {
            Debug.Log("Selected card: " + cardIndex);
            if (shuffledCards[cardIndex].isGoodCard)
            {
                ShowSuccessfulPanel();
            }
            else
            {
                ShowUnsuccesfulPanel(cards[cardIndex].deathScript);
            }

        }

        private void ShowSuccessfulPanel()
        {
            cardPanel.SetActive(false);
            succesfulPanel.SetActive(true);
        }

        private void ShowUnsuccesfulPanel(string deathScript)
        {
            cardPanel.SetActive(false);
            unsuccesfulPanel.SetActive(true);
            if (deathScript != "")
            {
                unsuccesfulPanel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = deathScript;
            }
            
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