using System;
using UnityEngine;

namespace _Scripts
{
    public class MusicManager : MonoBehaviour
    {
        
        private bool _musicOnStartUp = true;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("MusicOnStartUp"))
            {
                _musicOnStartUp = PlayerPrefs.GetInt("MusicOnStartUp") == 1;
            }
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (_musicOnStartUp)
            {
                _audioSource.Play();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMusic();
            }
        }

        private void ToggleMusic()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
            else
            {
                _audioSource.Play();
            }

        }
        

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("MusicOnStartUp", _musicOnStartUp ? 1 : 0);
            PlayerPrefs.Save();
        }
        

    }
}