using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countDownText;


    private void Start() {
        KitchenGameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;

        Hide();
    }

    private void Instance_OnGameStateChange(object sender, KitchenGameManager.OnStateChangedEventArgs e) {
        if (KitchenGameManager.Instance.IsCountDownToStartActive()) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Update() {
        countDownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountDownToStartTimer()).ToString();
    }
}
