using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Objects
{
    public class PipeEnd : MonoBehaviour
    {
        
        public Sprite fixedPipedEnd;
        public Sprite brokenPipeEnd;
        private SpriteRenderer _pipeSpriteRenderer;
        private SpriteRenderer _steamSpriteRenderer;
        private AudioSource _steamAudio;
        public GameObject steam;
        public GameObject fixSymbolObject;
        private Animator _steamAnimator;
        
        [SerializeField] private bool isBroken;
        [SerializeField] private bool isRepairable;

        private void Awake()
        {
            _pipeSpriteRenderer= GetComponent<SpriteRenderer>();
            _steamAudio = steam.GetComponentInChildren<AudioSource>();
        }


        private void Start()
        {
            _steamSpriteRenderer = steam.GetComponent<SpriteRenderer>();
            _steamAnimator =steam.GetComponent<Animator>();
            Invoke(nameof(DestroyPipe), Random.Range(5f, 10f));
            FixPipe();
        }
    
        public void DestroyPipe()
        {
            ToggleSteam(true); 
            _steamAnimator.Play("Steam");
            _steamAudio.Play();
            isBroken = true;
        }

        private void FixPipe()
        {
            ToggleSteam(false); 
                isBroken = false;
                _steamAudio.Stop();
        }
        
        
        private void ToggleSteam(bool state)
        {
            _pipeSpriteRenderer.sprite = state ? brokenPipeEnd : fixedPipedEnd;
            _steamSpriteRenderer.enabled = state;
            _steamAnimator.enabled = state;
        }
        
        /// <summary>
        /// Repairs the pipe, if it is repairable. Is called when the player is in the trigger area
        /// </summary>
        public void RepairPipe()
        {
            if (isRepairable)
            {
                FixPipe();
            }
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isBroken && other.CompareTag("Player"))
            {
                isRepairable = true;
                fixSymbolObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isRepairable = false;
                fixSymbolObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
