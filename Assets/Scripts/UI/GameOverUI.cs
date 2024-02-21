using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start() {
        KitchenGameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;

        recipesDeliveredText.text = "0";
        
        Hide();
    }

    private void Instance_OnGameStateChange(object sender, KitchenGameManager.OnStateChangedEventArgs e) {
        if (e.state == KitchenGameManager.EnumState.GameOver) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        recipesDeliveredText.text = DeliveryManager.Instance.RecipesSuccesfullyDelivered.ToString();

        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
