using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyDropSomething;

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject presentedObject;

    public virtual void Interact(Player player) {
        Debug.Log(this + ": Interact");
    }

    public virtual void InteractAlternate(Player player) {
        Debug.Log(this + ": InteractAlternate");
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetPresentedObject(KitchenObject kitchenObject) {
        presentedObject = kitchenObject;

        if (presentedObject != null) {
            OnAnyDropSomething?.Invoke(this, EventArgs.Empty);
        }
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
