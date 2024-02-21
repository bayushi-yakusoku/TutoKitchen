using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;


    private void Start() {
        KitchenGameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        mainMenuButton.onClick.AddListener(MainMenuAction);
        resumeButton.onClick.AddListener(ResumeAction);
        
        Hide();
    }

    private void Instance_OnGameStateChange(object sender, KitchenGameManager.OnStateChangedEventArgs e) {
        if (e.state == KitchenGameManager.EnumState.GamePause) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void ResumeAction() {
        KitchenGameManager.Instance.TogglePause();
    }

    private void MainMenuAction() {
        Loader.LoadScene(Loader.EnumScene.MainMenuScene);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
