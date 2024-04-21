using Unity.Netcode;
using UnityEngine;

public sealed class SoundManager : MonoBehaviour {
    // Make it Singleton:
    public static SoundManager Singleton { get; private set; }

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
        if (Singleton != null) {
            Debug.LogWarning(this + ": There is more than one SoundManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Singleton = this;

        GlobalVolume = PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_VOLUME, 0.5f);
    }

    private void Start() {
        DeliveryManager.Singleton.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Singleton.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;

        CuttingCounter.OnPlayerInteractAlternateAnyCuttingCounter += CuttingCounter_OnPlayerInteractAlternateAnyCuttingCounter;

        BaseCounter.OnAnyDropSomething += BaseCounter_OnDropSomething;

        TrashCounter.OnAnyTrashSomething += TrashCounter_OnAnyTrashSomething;
    }

    public void RegisterPlayer(Player player) {
        Debug.Log(this + ": add player:" + player);

        player.OnPlayerPickedSomething -= Player_OnPlayerPickedSomething;
        player.OnPlayerPickedSomething += Player_OnPlayerPickedSomething;
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

    private void Player_OnPlayerPickedSomething(object sender, System.EventArgs e) {
        if (sender is Player player) {
            PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
        }
    }

    private void CuttingCounter_OnPlayerInteractAlternateAnyCuttingCounter(object sender, System.EventArgs e) {
        if (sender is CuttingCounter counter) {
            PlaySound(audioClipRefsSO.chop, counter.transform.position);
        }
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e) {
        if (sender is DeliveryCounter counter) {
            PlaySound(audioClipRefsSO.deliveryFail, counter.transform.position);
        }
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e) {
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

    public void PlayCountDownSound() {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);

    }

    public void PlayWarningSound(Vector3 position) {
        PlaySound(audioClipRefsSO.warning, position);

    }
}
