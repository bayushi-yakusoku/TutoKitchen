using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
    private const string CUT = "Cut";
    private int hashCut;

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        hashCut = Animator.StringToHash(CUT);
    }

    // Start is called before the first frame update
    void Start() {
        cuttingCounter.OnPlayerInteractAlternateCuttingCounter += CuttingCounter_OnPlayerInteractAlternateCuttingCounter;
    }

    private void CuttingCounter_OnPlayerInteractAlternateCuttingCounter(object sender, System.EventArgs e) {
        animator.SetBool(hashCut, true);
    }
}
