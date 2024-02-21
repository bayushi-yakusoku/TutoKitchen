using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        Debug.Log(this + ": Interact");

        if (player.HasPresentedObject()) {
            Debug.Log(this + ": Player has an object");

            if (HasPresentedObject()) {
                Debug.Log(this + ": Counter has an object");

                if (player.GetPresentedObject() is PlateKitchenObject playerPlate) {
                    Debug.Log(this + ": player is holding a plate");

                    if (playerPlate.TryAddIngredient(GetPresentedObject().GetKitchenObjectSO())) {
                        GetPresentedObject().DestroySelf();
                    }
                }
                else if (GetPresentedObject() is PlateKitchenObject couterPlate) {
                    Debug.Log(this + ": counter is presenting a plate");

                    if (couterPlate.TryAddIngredient(player.GetPresentedObject().GetKitchenObjectSO())) {
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
