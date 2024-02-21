using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(PlayButtonClick);
        quitButton.onClick.AddListener(QuitButtonClick);

        Time.timeScale = 1f;
    }

    private void PlayButtonClick() {
        Loader.LoadScene(Loader.EnumScene.GameScene);
    }

    private void QuitButtonClick() {
        Application.Quit();
    }
}
