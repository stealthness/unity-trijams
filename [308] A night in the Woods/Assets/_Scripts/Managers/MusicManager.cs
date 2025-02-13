
using UnityEngine;

namespace _Scripts.Managers
{
    
    /// <summary>
    /// Manages the music in the game.
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        
        private bool _musicOnStartUp = true;
        private AudioSource _audioSource;

        private void Awake()
        {

            _audioSource = GetComponent<AudioSource>();
            LoadMusicPref();
        }

        private void Start()
        {

            if (_musicOnStartUp)
            {
                _audioSource.Play();
                
            }
            else
            {
                _audioSource.Stop();
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
                _musicOnStartUp = false;
                _audioSource.Pause();
            }
            else
            {
                _musicOnStartUp = true;
                _audioSource.Play();
            }
            SaveMusicPref();

        }
        

        private void OnApplicationQuit()
        {
            SaveMusicPref();
        }
        
        
        private void LoadMusicPref()
        {
            if (PlayerPrefs.HasKey("MusicOnStartUp"))
            {
                _musicOnStartUp = PlayerPrefs.GetInt("MusicOnStartUp") == 1;
            }
        }

        private void SaveMusicPref()
        {
            PlayerPrefs.SetInt("MusicOnStartUp", _musicOnStartUp ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}