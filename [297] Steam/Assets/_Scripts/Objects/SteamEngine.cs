using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Objects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SteamEngine : MonoBehaviour
    {
        public Sprite engineOnSprite;
        public Sprite engineOffSprite;
        [SerializeField] private bool playerInReach;
        [SerializeField] private bool engineOn = false;
        
        private AudioSource _audioSource;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.Stop();
        }

        private void Start()
        {
            SetEngine(engineOn);
        }

        private void SetEngine(bool isOn)
        {
            _spriteRenderer.sprite = isOn? engineOnSprite : engineOffSprite;
            if (isOn)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }


        void Update()
        {
            if (playerInReach)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartEngine();
                }
            }
        }
        
        public void StartEngine()
        {
            Debug.Log("Engine started");
            engineOn = true;
            SetEngine(engineOn);
            SteamManager.Instance.AddPressure(20);
            Exit.Instance.EngineFixed();

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartMenuManager.Instance.ShowMessage("Press E to start the engine");
                playerInReach = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            playerInReach = false;
        }
    }
}