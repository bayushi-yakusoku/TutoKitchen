using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private bool playWarningSound = false;
    private float warningSoundFrequency = 0.2f;
    private float warningSoundDelay = 0f;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;


        Hide();
    }

    private void Update() {
        if (playWarningSound) {
            warningSoundDelay += Time.deltaTime;
            if (warningSoundDelay >= warningSoundFrequency) {
                warningSoundDelay = 0f;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }


    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if (e.state != StoveCounter.EnumState.Fried) {
            Hide();
            playWarningSound = false;
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        if (e.progressNormalized >= .5f && stoveCounter.State == StoveCounter.EnumState.Fried) {
            Show();
            playWarningSound = true;
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
