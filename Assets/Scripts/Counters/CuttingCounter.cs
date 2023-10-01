using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int _currentCount = 0;
    private int CurrentCutCount { 
        get => _currentCount; 
        
        set { 
            _currentCount = value;

            if (_currentCount == 0) {
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                    progressNormalized = 0f
                }
                );
            }
        } 
    }

    public event EventHandler OnPlayerInteractAlternateCuttingCounter;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }

    public override void Interact(Player player) {
        Debug.Log(this + ": Interact");

        if (player.HasPresentedObject()) {
            Debug.Log(this + ": Player has an object");

            if (!HasPresentedObject()) {
                Debug.Log(this + ": Counter do NOT have an object");

                if (HasRecipeWithInput(player.GetPresentedObject().GetKitchenObjectSO())) {
                    Debug.Log(this + ": Recipe found");
                    player.GetPresentedObject().Owner = this;

                    CurrentCutCount = 0;
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

                CurrentCutCount = 0;
            }

        }
    }

    public override void InteractAlternate(Player player) {
        Debug.Log(this + ": InteractAlternate");

        if (HasPresentedObject()) {
            Debug.Log(this + ": Counter has an object");

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetPresentedObject()
                                                                            .GetKitchenObjectSO()
                                                                         );

            if (cuttingRecipeSO is null) {
                Debug.Log(this + ": no cutting recipe found");

                return;
            }

            OnPlayerInteractAlternateCuttingCounter?.Invoke(this, EventArgs.Empty);

            CurrentCutCount++;

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { 
                    progressNormalized = (float) CurrentCutCount / cuttingRecipeSO.CutCountNeeded
                }
            );

            if (CurrentCutCount >= cuttingRecipeSO.CutCountNeeded) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetPresentedObject()
                                                                            .GetKitchenObjectSO()
                                                                         );

                if (outputKitchenObjectSO is not null) {
                    GetPresentedObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }

                CurrentCutCount = 0;
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSO is not null) {
            return cuttingRecipeSO.output;
        }

        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                Debug.Log(this + ": found recipe, input is " + cuttingRecipeSO.input +
                    " output is " + cuttingRecipeSO.output);

                return cuttingRecipeSO;
            }
        }

        Debug.Log(this + ": No recipe found");

        return null;
    }
}
