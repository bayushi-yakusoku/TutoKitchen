using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    private IKitchenObjectParent _owner;
    public IKitchenObjectParent Owner {
        get => _owner;

        set {
            if (_owner != null) {
                _owner.ClearPresentedObject();
            }

            _owner = value;

            if (_owner.HasPresentedObject()) {
                Debug.LogError(_owner + ": has already a Presented object!");
            }

            _owner.SetPresentedObject(this);

            followTarget = _owner.GetKitchenObjectFollowTransform();
        }
    }

    public void DestroySelf() {
        Debug.Log(this + ": Destroying self");

        _owner?.ClearPresentedObject();

        Destroy(gameObject);
    }

    private Transform followTarget;

    private void Update() {
        if (followTarget != null) {
            transform.SetPositionAndRotation(followTarget.position, followTarget.rotation);
        }
    }
}
