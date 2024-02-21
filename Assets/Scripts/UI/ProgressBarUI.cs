using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private float currentProgress = 0f;

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
