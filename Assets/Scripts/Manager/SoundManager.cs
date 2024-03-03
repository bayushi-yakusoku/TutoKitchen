using UnityEngine;

public sealed class SoundManager : MonoBehaviour {
    // Make it Singleton:
    public static SoundManager Instance { get; private set; }

    private const string PLAYER_PREF_SOUND_VOLUME = "PlayerPrefVolume";

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float _globalVolume = 1f;
    public float GlobalVolume {
        get => _globalVolume;

        set { 
            _globalVolume = value;
            PlayerPrefs.SetFloat(PLAYER_PREF_SOUND_VOLUME, _globalVolume);
            PlayerPrefs.Save();
        } 
    }

    private void Awake() {
        // Singleton simple implementation:
        if (Instance != null) {
            Debug.LogWarning(this + ": There is more than one SoundManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Instance = this;

        GlobalVolume = PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_VOLUME, 0.5f);
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliverySuccess += Instance_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += Instance_OnDeliveryFailed;
        CuttingCounter.OnPlayerInteractAlternateAnyCuttingCounter += CuttingCounter_OnPlayerInteractAlternateAnyCuttingCounter;
        Player.Instance.OnPlayerPickedSomething += Instance_OnPlayerPickedSomething;
        BaseCounter.OnAnyDropSomething += BaseCounter_OnDropSomething;
        TrashCounter.OnAnyTrashSomething += TrashCounter_OnAnyTrashSomething;
    }

    private void TrashCounter_OnAnyTrashSomething(object sender, System.EventArgs e) {
        if (sender is TrashCounter counter) {
            PlaySound(audioClipRefsSO.trash, counter.transform.position);
        }
    }

    private void BaseCounter_OnDropSomething(object sender, System.EventArgs e) {
        if (sender is BaseCounter counter) {
            PlaySound(audioClipRefsSO.objectDrop, counter.transform.position);
        }
    }

    private void Instance_OnPlayerPickedSomething(object sender, System.EventArgs e) {
        if (sender is Player player) {
            PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
        }
    }

    private void CuttingCounter_OnPlayerInteractAlternateAnyCuttingCounter(object sender, System.EventArgs e) {
        if (sender is CuttingCounter counter) {
            PlaySound(audioClipRefsSO.chop, counter.transform.position);
        }
    }

    private void Instance_OnDeliveryFailed(object sender, System.EventArgs e) {
        if (sender is DeliveryCounter counter) {
            PlaySound(audioClipRefsSO.deliveryFail, counter.transform.position);
        }
    }

    private void Instance_OnDeliverySuccess(object sender, System.EventArgs e) {
        if (sender is DeliveryCounter counter) {
            PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
        }
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position) {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, GlobalVolume);
    }

    public void PlayFootStepsSound(Vector3 position) {
        PlaySound(audioClipRefsSO.footStep, position);

    }
}
