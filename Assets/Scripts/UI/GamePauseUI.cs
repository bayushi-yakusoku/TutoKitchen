using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;


    private void Start() {
        KitchenGameManager.Instance.OnGameStateChange += KitchenGameManager_OnGameStateChange;
        mainMenuButton.onClick.AddListener(MainMenuButtonClick);
        resumeButton.onClick.AddListener(ResumeButtonClick);
        optionsButton.onClick.AddListener(OptionsButtonClick);
        
        Hide();
    }

    private void KitchenGameManager_OnGameStateChange(object sender, KitchenGameManager.OnStateChangedEventArgs e) {
        if (e.state == KitchenGameManager.EnumState.GamePause) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void ResumeButtonClick() {
        KitchenGameManager.Instance.TogglePause();
    }

    private void MainMenuButtonClick() {
        Loader.LoadScene(Loader.EnumScene.MainMenuScene);
    }

    private void OptionsButtonClick() {
        KitchenGameManager.Instance.ToggleOptions();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
