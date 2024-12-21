using UnityEngine;

namespace _Scripts.Managers
{
    public class MusicManager : MonoBehaviour
    {
    
        public AudioSource audioSource;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (audioSource.isPlaying)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    audioSource.Pause();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    audioSource.UnPause();
                }
            }
        }
    }
}
