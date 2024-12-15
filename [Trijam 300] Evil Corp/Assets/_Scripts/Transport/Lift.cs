using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Transport
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
        
        private PlayerMovement2D player;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && isOpen)
            {
                player.MoveTo(player.transform.position - transform.position + destination.position);
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
