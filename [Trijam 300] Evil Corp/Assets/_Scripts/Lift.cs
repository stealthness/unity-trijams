
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Lift : MonoBehaviour
    {
        public Sprite openSprite;
        public Sprite closedSprite;
        public bool isOpen = false;
        public string name;

        [SerializeField] public SpriteRenderer sign;
        [SerializeField] public Transform destination;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && isOpen)
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.position =player.transform.position - transform.position + destination.position;
            }
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GetComponent<SpriteRenderer>().sprite = openSprite;
                sign.enabled = true;
                isOpen = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GetComponent<SpriteRenderer>().sprite = closedSprite;
                isOpen = false;
                sign.enabled = false;
            }
        }
    }
}
