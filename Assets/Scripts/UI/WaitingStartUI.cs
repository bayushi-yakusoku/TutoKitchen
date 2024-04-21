using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingStartUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI tutoMoveUpText;
    [SerializeField] private TextMeshProUGUI tutoMoveDownText;
    [SerializeField] private TextMeshProUGUI tutoMoveLeftText;
    [SerializeField] private TextMeshProUGUI tutoMoveRightText;
    [SerializeField] private TextMeshProUGUI tutoInteractText;
    [SerializeField] private TextMeshProUGUI tutoAlternateText;
    [SerializeField] private TextMeshProUGUI tutoPauseText;

    private void Start() {
        Show();

        KitchenGameManager.Singleton.OnGameStateChange += KitchenGameObject_OnGameStateChange;

    }

    private void KitchenGameObject_OnGameStateChange(object sender, KitchenGameManager.OnStateChangedEventArgs e) {
        if (e.state == KitchenGameManager.EnumState.WaitingToStart) {
            Show();
        }
        else {
            Hide();
        }

    }

    private void Show() {
        UpdateUI();
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateUI() {
        tutoMoveUpText.text     = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveUp);
        tutoMoveDownText.text   = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveDown);
        tutoMoveLeftText.text   = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveLeft);
        tutoMoveRightText.text  = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.MoveRight);
        tutoInteractText.text   = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.Interact);
        tutoAlternateText.text  = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.Alternate);
        tutoPauseText.text      = GameInputManager.Singleton.GetBindingText(GameInputManager.EnumBinding.Pause);
    }
}
