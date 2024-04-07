using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Singleton { get; private set; }

    private CinemachineTargetGroup playersTargetGroup;

    private void Awake() {
        if (Singleton != null) {
            Debug.LogWarning(this + ": There is more than one CameraManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Singleton = this;

        playersTargetGroup = GetComponent<CinemachineTargetGroup>();
    }

    public void AddPlayer(Transform player) {
        playersTargetGroup.AddMember(player, 1, 0);
    }
}
