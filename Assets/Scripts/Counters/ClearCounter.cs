using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        Debug.Log(this + ": Interact");

        if (player.HasPresentedObject()) {
            Debug.Log(this + ": Player has an object");

            if (HasPresentedObject()) {
                Debug.Log(this + ": Counter has an object");

                if (player.GetPresentedObject().GetType() == typeof(PlateKitchenObject)) {
                    Debug.Log(this + ": player is holding a plate");

                    PlateKitchenObject plate = (PlateKitchenObject)player.GetPresentedObject();
                    if (plate.TryAddIngredient(GetPresentedObject().GetKitchenObjectSO())) {
                        GetPresentedObject().DestroySelf();
                    }
                }
                else if (GetPresentedObject().GetType() == typeof(PlateKitchenObject)) {
                    Debug.Log(this + ": counter is presenting a plate");

                    PlateKitchenObject plate = (PlateKitchenObject)GetPresentedObject();
                    if (plate.TryAddIngredient(player.GetPresentedObject().GetKitchenObjectSO())) {
                        player.GetPresentedObject().DestroySelf();
                    }
                }
            }
            else {
                Debug.Log(this + ": Counter do NOT have an object");

                player.GetPresentedObject().Owner = this;
            }
        }
        else {
            Debug.Log(this + ": Player do NOT have an object");

            if (HasPresentedObject()) {
                Debug.Log(this + ": Counter has an object");

                GetPresentedObject().Owner = player;
            }

        }
    }
}
