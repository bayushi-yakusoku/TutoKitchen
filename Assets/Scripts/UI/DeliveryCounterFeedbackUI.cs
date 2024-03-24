using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryCounterFeedbackUI : MonoBehaviour {

    [SerializeField] private Image background;
    [SerializeField] private Color successBackgroundColor;
    [SerializeField] private Color failedBackgroundColor;

    [Space(10)]

    [SerializeField] private Image feedbackImage;
    [SerializeField] private Texture successImageTexture;
    [SerializeField] private Color successImageColor;
    [SerializeField] private Texture failedImageTexture;
    [SerializeField] private Color failedImageColor;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI feedbackText;


}
