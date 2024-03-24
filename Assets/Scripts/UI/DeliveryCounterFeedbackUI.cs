using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryCounterFeedbackUI : MonoBehaviour {

    private const string SUCCESS_RESULT = "Delivery\nSuccess";
    private const string FAILED_RESULT = "Delivery\nFailed";

    [SerializeField] private Image background;
    [SerializeField] private Color successBackgroundColor;
    [SerializeField] private Color failedBackgroundColor;

    [Space(10)]

    [SerializeField] private Image feedbackImage;
    [SerializeField] private Sprite successImageSprite;
    [SerializeField] private Color successImageColor;
    [SerializeField] private Sprite failedImageSprite;
    [SerializeField] private Color failedImageColor;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI feedbackText;


    private void Start() {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e) {
        DisplayFailed();
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e) {
        DisplaySuccess();
    }


    private void DisplaySuccess() {
        background.color = successBackgroundColor;

        feedbackImage.sprite = successImageSprite;
        feedbackImage.color = successImageColor;

        feedbackText.text = SUCCESS_RESULT;
    }

    private void DisplayFailed() {
        background.color = failedBackgroundColor;

        feedbackImage.sprite = failedImageSprite;
        feedbackImage.color = failedImageColor;

        feedbackText.text = FAILED_RESULT;
    }

}
