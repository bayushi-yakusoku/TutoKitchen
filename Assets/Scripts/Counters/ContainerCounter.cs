using System;
using Unity.Netcode;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player) {
        Debug.Log(this + ": Interact");

        if (!player.HasPresentedObject()) {
            Debug.Log(this + ": player do NOT have an object");

            //KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            MultiplayerManager.Singleton.SpawnKitchenObject(kitchenObjectSO, player);

            GrabbedObjectRpc();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void GrabbedObjectRpc() {
        Debug.Log(this + ": ClientsAndHost - fire event OnPlayerGrabbedObject");

        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
