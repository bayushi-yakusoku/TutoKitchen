using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject presentedObject;

    public virtual void Interact(Player player) { }

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
