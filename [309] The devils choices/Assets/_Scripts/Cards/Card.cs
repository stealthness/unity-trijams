using UnityEngine;

namespace _Scripts.Cards
{
    [CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Card", order = 1)]
    public class Card : ScriptableObject
    {
        public string cardName;
        public Sprite cardImage;
        public int cardIndex;
        public bool isGoodCard;
    }


}