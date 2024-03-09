using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private Button mainMenuButton;


    private void Start() {
        KitchenGameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        mainMenuButton.onClick.AddListener(MainMenuButtonClick);

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
        mainMenuButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void MainMenuButtonClick() {
        Loader.LoadScene(Loader.EnumScene.MainMenuScene);
    }
}
