
using UnityEngine;

namespace _Scripts.Managers
{
    
    /// <summary>
    /// Manages the music in the game.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        private const KeyCode ToggleMusicKey = KeyCode.M;
        private const string MusicOnStartUpKey = "MusicOnStartUp";
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
            if (Input.GetKeyDown(ToggleMusicKey))
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
            if (PlayerPrefs.HasKey(MusicOnStartUpKey))
            {
                _musicOnStartUp = PlayerPrefs.GetInt(MusicOnStartUpKey) == 1;
            }
        }

        private void SaveMusicPref()
        {
            PlayerPrefs.SetInt(MusicOnStartUpKey, _musicOnStartUp ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}