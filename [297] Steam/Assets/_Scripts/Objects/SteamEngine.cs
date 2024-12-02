using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Objects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SteamEngine : MonoBehaviour
    {
        private AudioSource _audioSource;
        public Sprite engineOnSprite;
        public Sprite engineOffSprite;
        [SerializeField] private bool _playerInReach;
        
        private BoxCollider2D _boxCollider2D;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = engineOffSprite;
            _audioSource.Stop();
            
        }

        
        void Update()
        {
            if (_playerInReach)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartEngine();
                }
            }
        }
        
        public void StartEngine()
        {
            _spriteRenderer.sprite = engineOnSprite;
            SteamManager.Instance.AddPressure(20);
            Exit.Instance.EngineFixed();
            _audioSource.Play();

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInReach = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _playerInReach = false;
        }
    }
}