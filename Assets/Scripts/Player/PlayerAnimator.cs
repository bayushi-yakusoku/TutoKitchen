using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour {
    private const string IS_WALKING = "IsWalking";

    private int isWalkingId;

    private Animator animator;

    [SerializeField] Player player;

    private void Awake() {
        animator = GetComponent<Animator>();
        isWalkingId = Animator.StringToHash(IS_WALKING);

        animator.SetBool(isWalkingId, player.IsWalking());
    }

    private void Update() {
        if (! IsOwner) {
            return;
        }

        animator.SetBool(isWalkingId, player.IsWalking());
    }
}

