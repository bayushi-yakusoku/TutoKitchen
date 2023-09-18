using System.Collections;
using System.Collections.Generic;
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
                Debug.LogError(_owner + " has already a Presented object!");
            }

            _owner.SetPresentedObject(this);

            transform.parent = _owner.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }
}
