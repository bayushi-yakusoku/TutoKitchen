using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter {
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;


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

            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetPresentedObject()
                                                                            .GetKitchenObjectSO()
                                                                         );

            if (fryingRecipeSO is null) {
                Debug.Log(this + ": no cutting recipe found");

                return;
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSO is not null) {
            return cuttingRecipeSO.output;
        }

        return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                Debug.Log(this + ": found recipe, input is " + fryingRecipeSO.input +
                    " output is " + fryingRecipeSO.output);

                return fryingRecipeSO;
            }
        }

        Debug.Log(this + ": No recipe found");

        return null;
    }
}
