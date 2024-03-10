using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    // Make it Singleton:
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREF_MUSIC_VOLUME = "PlayerPrefMusicVolume";

    private AudioSource gameMusic;

    private void Awake() {
        // Singleton simple implementation:
        if (Instance != null) {
            Debug.LogWarning(this + ": There is more than one MusicManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Instance = this;

        gameMusic = GetComponent<AudioSource>();

        GlobalVolume = PlayerPrefs.GetFloat(PLAYER_PREF_MUSIC_VOLUME, 0.3f);

        gameMusic.volume = GlobalVolume;

        gameMusic.Pause();
    }

    private float _globalVolume = 1f;
    public float GlobalVolume { 
        get => _globalVolume; 

        set { 
            _globalVolume = value;

            gameMusic.volume = _globalVolume;
            PlayerPrefs.SetFloat(PLAYER_PREF_MUSIC_VOLUME, _globalVolume);
            PlayerPrefs.Save();
        } 
    }

    public void Play() {
        gameMusic.Play();
    }
}
