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


    [SerializeField] private Button moveUpButton;
    [SerializeField] private TextMeshProUGUI moveUpText;

    [SerializeField] private Button moveDownButton;
    [SerializeField] private TextMeshProUGUI moveDownText;

    [SerializeField] private Button moveLeftButton;
    [SerializeField] private TextMeshProUGUI moveLeftText;

    [SerializeField] private Button moveRightButton;
    [SerializeField] private TextMeshProUGUI moveRightText;

    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;

    [SerializeField] private Button alternateButton;
    [SerializeField] private TextMeshProUGUI alternateText;

    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Button backButton;

    private void Start() {
        KitchenGameManager.Singleton.OnGameStateChange += KitchenGameManager_OnGameStateChange;
        soundButton.onClick.AddListener(SoundButtonClick);
        musicButton.onClick.AddListener(MusicButtonClick);
        backButton.onClick.AddListener(BackButtonClick);

        moveUpButton.onClick.AddListener(() => KitchenGameManager.Singleton.SetBinding(GameInputManager.EnumBinding.MoveUp));
        moveDownButton.onClick.AddListener(() => KitchenGameManager.Singleton.SetBinding(GameInputManager.EnumBinding.MoveDown));
        moveLeftButton.onClick.AddListener(() => KitchenGameManager.Singleton.SetBinding(GameInputManager.EnumBinding.MoveLeft));
        moveRightButton.onClick.AddListener(() => KitchenGameManager.Singleton.SetBinding(GameInputManager.EnumBinding.MoveRight));
        interactButton.onClick.AddListener(() => KitchenGameManager.Singleton.SetBinding(GameInputManager.EnumBinding.Interact));
        alternateButton.onClick.AddListener(() => KitchenGameManager.Singleton.SetBinding(GameInputManager.EnumBinding.Alternate));
        pauseButton.onClick.AddListener(() => KitchenGameManager.Singleton.SetBinding(GameInputManager.EnumBinding.Pause));

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
        SoundManager.Singleton.GlobalVolume += 0.1f;

        if (SoundManager.Singleton.GlobalVolume > 1f) {
            SoundManager.Singleton.GlobalVolume = 0f;
        }

        UpdateUI();
    }

    private void MusicButtonClick() {
        MusicManager.Singleton.GlobalVolume += 0.1f;

        if (MusicManager.Singleton.GlobalVolume > 1f) {
            MusicManager.Singleton.GlobalVolume = 0f;
        }

        UpdateUI();
    }

    private void BackButtonClick() {
        KitchenGameManager.Singleton.ToggleOptions();
    }

    private void Show() {
        UpdateUI();
        gameObject.SetActive(true);
        soundButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateUI() {
        soundButtonText.text = "Sound updated:" + Mathf.Round(SoundManager.Singleton.GlobalVolume * 10f);
        musicButtonText.text = "Music updated:" + Mathf.Round(MusicManager.Singleton.GlobalVolume * 10f);

        moveUpText.text     = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveUp);
        moveDownText.text   = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveDown);
        moveLeftText.text   = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveLeft);
        moveRightText.text  = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveRight);
        interactText.text   = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.Interact);
        alternateText.text  = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.Alternate);
        pauseText.text      = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.Pause);

    }
}
