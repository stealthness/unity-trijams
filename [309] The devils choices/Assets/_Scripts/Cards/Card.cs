using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Cards
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Card", order = 1)]
    public class Card : ScriptableObject
    {
        public string cardName;
        public Sprite cardImage;
        public int cardIndex;
        public bool isGoodCard;

        [TextArea]
        public string deathScript;
        [TextArea]
        public string cardDescription;


        public void DrawCard(GameObject card, int index)
        {
            var button = card.GetComponent<Button>();
            cardIndex = index;

            if (button != null)
            {
                Debug.Log(button.ToString());
                var texts = button.GetComponentsInChildren<TextMeshProUGUI>();
                var images = button.GetComponentsInChildren<Image>();
                card.transform.Find("CardImage").GetComponent<Image>().sprite = cardImage;
                

                foreach (var image in images)
                {
                    Debug.Log("1: " + image.ToString());
                    Debug.Log("2: " + image.name);
                }
                
                if (images.Length > 0)
                {
                    images[0].sprite = cardImage;
                }
                else
                {
                    Debug.Log("images.length " + images.Length);
                }
                
                if (texts.Length == 2)
                {
                    texts[0].text = cardName;
                    texts[1].text = cardDescription;
                }
                else
                {
                    Debug.Log("texts.length " + texts.Length);
                }
            }
        }
    }



}