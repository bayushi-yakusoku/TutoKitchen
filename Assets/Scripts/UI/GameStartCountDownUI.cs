using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countDownText;

    private const string NUMBER_POPUP_TRIGGER = "NumberPopup";

    private Animator animator;

    private int previousCountDownNumber;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

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
        int countDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownToStartTimer());
        countDownText.text = countDownNumber.ToString();

        if (countDownNumber != previousCountDownNumber) {
            previousCountDownNumber = countDownNumber;

            animator.SetTrigger(NUMBER_POPUP_TRIGGER);
            SoundManager.Instance.PlayCountDownSound();
        }
    }
}
