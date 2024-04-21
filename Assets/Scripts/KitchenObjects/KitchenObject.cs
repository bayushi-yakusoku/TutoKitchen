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
            if (_owner == value) {
                Debug.Log(this + ": local - update with same value canceled");

                return;
            }

            UpdateInfoRpc(this.NetworkObject, value.GetNetworkRef());
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void UpdateInfoRpc(NetworkObjectReference me, NetworkObjectReference newOwner) {
        Debug.Log(this + ": ClientsAndHost - Update kitchen object properties");

        me.TryGet(out NetworkObject meObject);
        KitchenObject meKit = meObject.GetComponent<KitchenObject>();
        
        newOwner.TryGet(out NetworkObject newOwnerObject);
        IKitchenObjectParent newIk = newOwnerObject.GetComponent<IKitchenObjectParent>();

        meKit.SetOwner(newIk);
    }

    private void SetOwner(IKitchenObjectParent value) {
        Debug.Log(this + ": local - Update kitchen object properties");

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
