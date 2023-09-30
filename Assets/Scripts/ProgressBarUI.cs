using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private float currentProgress = 0f;

    public void Start() {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        Hide();
    }

    public void Update() {
        barImage.fillAmount = currentProgress;
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
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
