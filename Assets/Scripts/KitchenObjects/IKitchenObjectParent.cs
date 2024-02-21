using UnityEngine;

public interface IKitchenObjectParent {

    public Transform GetKitchenObjectFollowTransform();
    public void SetPresentedObject(KitchenObject kitchenObject);
    public KitchenObject GetPresentedObject();
    public void ClearPresentedObject();
    public bool HasPresentedObject();
}
