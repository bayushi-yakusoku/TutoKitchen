using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    // Make it Singleton:
    public static MusicManager Instance { get; private set; }

    private AudioSource gameMusic;

    private void Awake() {
        // Singleton simple implementation:
        if (Instance != null) {
            Debug.LogWarning(this + ": There is more than one MusicManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Instance = this;

        gameMusic = GetComponent<AudioSource>();
    }

    private float _globalVolume = 1f;
    public float GlobalVolume { 
        get => _globalVolume; 

        set { 
            _globalVolume = value;

            gameMusic.volume = _globalVolume;
        } 
    }
}
