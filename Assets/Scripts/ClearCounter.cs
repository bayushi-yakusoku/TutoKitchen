using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    [SerializeField] private ClearCounter destinationCounter;
    [SerializeField] bool Testing;

    private KitchenObject _presentedObject;

    public void Interact() {
        if (_presentedObject is null) {
            Debug.Log(this + ": Interact");

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTransform.GetComponent<KitchenObject>().OwnerCounter = this;

            Debug.Log(_presentedObject.GetKitchenObjectSO().objectName);
        }
        else {
            Debug.Log("OwnerCounter: " + _presentedObject.OwnerCounter);

            if (Testing && destinationCounter) {
                Debug.Log("Moving object...");
                _presentedObject.OwnerCounter = destinationCounter;
            }
        }
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public KitchenObject PresentedObject {
        get => _presentedObject;

        set {
            _presentedObject = value;
        }
    }

}
