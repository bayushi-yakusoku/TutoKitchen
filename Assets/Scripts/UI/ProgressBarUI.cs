using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private const string IS_FLASHING = "IsFlashing";
    private Animator animator;

    private IHasProgress hasProgress;

    private float currentProgress = 0f;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null) {
            Debug.LogError(this + ": Field do NOT have IHasProgress interface");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        Hide();
    }

    public void Update() {
        barImage.fillAmount = currentProgress;
        if (hasProgress.IsFlashing() && currentProgress >= 0.5f) {
            animator.SetBool(IS_FLASHING, true);
        }
        else {
            animator.SetBool(IS_FLASHING, false);
        }
    }

    private void HasProgress_OnProgressChanged(object sender,
        IHasProgress.OnProgressChangedEventArgs e) {
        currentProgress = e.progressNormalized;

        if (currentProgress == 0f || currentProgress == 1f) {
            Hide();
        }
        else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
