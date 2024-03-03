using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour {
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;

    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;

    [SerializeField] private Button backButton;

    private void Start() {
        KitchenGameManager.Instance.OnGameStateChange += KitchenGameManager_OnGameStateChange;
        soundButton.onClick.AddListener(SoundButtonClick);
        musicButton.onClick.AddListener(MusicButtonClick);
        backButton.onClick.AddListener(BackButtonClick);

        UpdateUI();
        Hide();
    }

    private void KitchenGameManager_OnGameStateChange(object sender, KitchenGameManager.OnStateChangedEventArgs e) {
        if (e.state == KitchenGameManager.EnumState.GameOptions) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void SoundButtonClick() {
        SoundManager.Instance.GlobalVolume += 0.1f;

        if (SoundManager.Instance.GlobalVolume > 1f) {
            SoundManager.Instance.GlobalVolume = 0f;
        }

        UpdateUI();
    }

    private void MusicButtonClick() {
        MusicManager.Instance.GlobalVolume += 0.1f;

        if (MusicManager.Instance.GlobalVolume > 1f) {
            MusicManager.Instance.GlobalVolume = 0f;
        }

        UpdateUI();
    }

    private void BackButtonClick() {
        KitchenGameManager.Instance.ToggleOptions();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateUI() {
        soundButtonText.text = "Sound updated:" + Mathf.Round(SoundManager.Instance.GlobalVolume * 10f);
        musicButtonText.text = "Music updated:" + Mathf.Round(MusicManager.Instance.GlobalVolume * 10f);
    }
}
