using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter _ownerCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public ClearCounter OwnerCounter {
        get => _ownerCounter;

        set {
            if (_ownerCounter) {
                _ownerCounter.PresentedObject = null;
            }

            _ownerCounter = value;

            if (_ownerCounter.PresentedObject) {
                Debug.LogError(_ownerCounter + " has already a Presented object!");
            }

            _ownerCounter.PresentedObject = this;

            transform.parent = _ownerCounter.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }
}
