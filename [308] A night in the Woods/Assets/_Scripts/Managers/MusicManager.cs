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
            else
            {
                SaveMusicPref();
            }
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("MusicOnStartUp"))
            {
                _musicOnStartUp = PlayerPrefs.GetInt("MusicOnStartUp") == 1;
            }
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
            SaveMusicPref();

        }
        

        private void OnApplicationQuit()
        {
            SaveMusicPref();
        }

        private void SaveMusicPref()
        {
            PlayerPrefs.SetInt("MusicOnStartUp", _musicOnStartUp ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}