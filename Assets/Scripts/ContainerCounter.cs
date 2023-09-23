using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player) {
        Debug.Log(this + ": Interact");

        if (!player.HasPresentedObject()) {
            Debug.Log(this + ": player do NOT have an object");

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

            kitchenObjectTransform.GetComponent<KitchenObject>().Owner = player;

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
