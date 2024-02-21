using UnityEngine;
using UnityEngine.UI;

public class PlayingClockUI : MonoBehaviour {
    [SerializeField] private Image playingClockImage;

    private void Start() {
        playingClockImage.fillAmount = 0.1f;
    }

    private void Update() {
        playingClockImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
