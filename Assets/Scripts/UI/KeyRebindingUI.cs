using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRebindingUI : MonoBehaviour {
    void Start() {
        KitchenGameManager.Instance.OnGameStateChange += KitchenGameManager_OnGameStateChange;

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

    void Update() {

    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
