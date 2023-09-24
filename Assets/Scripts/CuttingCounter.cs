using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        Debug.Log(this + ": Interact");

        if (player.HasPresentedObject()) {
            Debug.Log(this + ": Player has an object");

            if (!HasPresentedObject()) {
                Debug.Log(this + ": Counter do NOT have an object");

                if (HasRecipeWithInput(player.GetPresentedObject().GetKitchenObjectSO())) {
                    Debug.Log(this + ": Recipe found");
                    player.GetPresentedObject().Owner = this;
                }
                else {
                    Debug.Log(this + ": NO recipe found");
                }
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

    public override void InteractAlternate(Player player) {
        Debug.Log(this + ": InteractAlternate");

        if (HasPresentedObject()) {
            Debug.Log(this + ": Counter has an object");

            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(
                    GetPresentedObject().GetKitchenObjectSO()
                );

            if (outputKitchenObjectSO is not null) {
                GetPresentedObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }

        return false;

    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                Debug.Log(this + ": input is " + cuttingRecipeSO.input +
                    " output is " + cuttingRecipeSO.output);

                return cuttingRecipeSO.output;
            }
        }

        Debug.Log(this + ": No recipe found");

        return null;
    }
}
