using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject presentedObject;

    public void Interact(Player player) {
        if (presentedObject is null) {
            Debug.Log(this + ": Interact");

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTransform.GetComponent<KitchenObject>().Owner = this;

            Debug.Log(presentedObject.GetKitchenObjectSO().objectName);
        }
        else {
            Debug.Log("OwnerCounter: " + presentedObject.Owner);

            if (!player.HasPresentedObject()) {
                presentedObject.Owner = player;

                return;
            }
        }
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetPresentedObject(KitchenObject kitchenObject) {
        presentedObject = kitchenObject;
    }

    public KitchenObject GetPresentedObject() {
        return presentedObject;
    }

    public void ClearPresentedObject() {
        presentedObject = null;
    }

    public bool HasPresentedObject() {
        return presentedObject != null;
    }
}
