using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryCounterFeedbackUI : MonoBehaviour {

    private const string TRIGGER_POPUP_FEEDBACK = "PopupDeliveryFeedback";

    private const string SUCCESS_RESULT = "Delivery\nSuccess";
    private const string FAILED_RESULT = "Delivery\nFailed";

    [SerializeField] private DeliveryCounter deliveryCounter;

    [Space(10)]

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


    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Singleton.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Singleton.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e) {
        if (sender != (object) deliveryCounter) {
            return;
        }

        DisplayFailed();
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e) {
        if (sender != (object) deliveryCounter) {
            return;
        }

        DisplaySuccess();
    }


    private void DisplaySuccess() {
        background.color = successBackgroundColor;

        feedbackImage.sprite = successImageSprite;
        feedbackImage.color = successImageColor;

        feedbackText.text = SUCCESS_RESULT;

        animator.SetTrigger(TRIGGER_POPUP_FEEDBACK);
    }

    private void DisplayFailed() {
        background.color = failedBackgroundColor;

        feedbackImage.sprite = failedImageSprite;
        feedbackImage.color = failedImageColor;

        feedbackText.text = FAILED_RESULT;

        animator.SetTrigger(TRIGGER_POPUP_FEEDBACK);
    }

}
