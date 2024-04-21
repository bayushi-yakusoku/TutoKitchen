using UnityEngine;

public class FollowTarget : MonoBehaviour {
    public Transform Target { get; set; }

    private void Update() {
        if (Target != null) {
            transform.SetPositionAndRotation(Target.position, Target.rotation);
        }
    }
}
