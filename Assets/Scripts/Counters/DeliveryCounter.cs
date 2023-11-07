using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (player.HasPresentedObject()) {
            Debug.Log(this + ": player is presenting an object");

            if (player.GetPresentedObject() is PlateKitchenObject plate) {
                Debug.Log(this + ": it's a plate");

                plate.DestroySelf();
            }
        }
    }
}
