using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private GameObject smokeGameObject;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        switch (e.state) {
            case StoveCounter.EnumState.Idle:
                stoveOnGameObject.SetActive(false);
                particlesGameObject.SetActive(false);
                smokeGameObject.SetActive(false);
                break;

            case StoveCounter.EnumState.Frying:
                stoveOnGameObject.SetActive(true);
                particlesGameObject.SetActive(false);
                smokeGameObject.SetActive(false);
                break;

            case StoveCounter.EnumState.Fried:
                stoveOnGameObject.SetActive(true);
                particlesGameObject.SetActive(true);
                smokeGameObject.SetActive(false);
                break;

            case StoveCounter.EnumState.Burned:
                stoveOnGameObject.SetActive(true);
                particlesGameObject.SetActive(true);
                smokeGameObject.SetActive(true);
                break;
        }
    }
}
