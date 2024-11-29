using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class PipeEnd : MonoBehaviour
    {
        
        public Sprite fixedPipedEnd;
        public Sprite brokenPipeEnd;
        private SpriteRenderer _pipeSpriteRenderer;
        private SpriteRenderer _steamSpriteRenderer;
        public GameObject steam;
        public GameObject fixSymbolObject;
        private Animator _steamAnimator;
        
        

        private void Awake()
        {
            _pipeSpriteRenderer= GetComponent<SpriteRenderer>();
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
        }

        public void FixPipe()
        {
            ToggleSteam(false);
        }
        
        private void ToggleSteam(bool state)
        {
            _pipeSpriteRenderer.sprite = state ? brokenPipeEnd : fixedPipedEnd;
            _steamSpriteRenderer.enabled = state;
            _steamAnimator.enabled = state;
        }
        
        
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player has reached the end of the pipe");
                fixSymbolObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                fixSymbolObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
