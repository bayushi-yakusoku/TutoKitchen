using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent _owner;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

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

            transform.parent = _owner.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    public void DestroySelf() {
        Debug.Log(this + ": Destroying self");

        _owner?.ClearPresentedObject();

        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(
        KitchenObjectSO kitchenObjectSO,
        IKitchenObjectParent kitchenObjectParent
        ) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.Owner = kitchenObjectParent;

        return kitchenObject;
    }
}
