using System;
using UnityEngine;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private bool _playMusicAtStart = true;
        [SerializeField] private bool _loopMusic = true;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (_playMusicAtStart)
            {
                PlayMusic();
            }
            _audioSource.loop = _loopMusic;
            
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMusic();
            }
        }

        private void PlayMusic()
        {
            _audioSource.Play();
        }
        
        private void ToggleMusic()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.Play();
            }
        }
    }
}