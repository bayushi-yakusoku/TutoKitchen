using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    public enum EnumState {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private EnumState _state;
    private EnumState State {
        get => _state;
        set {
            _state = value;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.State });
        }
    }

    private FryingRecipeSO fryingRecipeSO;
    private float fryingTimer;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public EnumState state;
    }


    private void Start() {
        State = EnumState.Idle;
        fryingTimer = 0f;

        Debug.Log(this + $": state is {State}");
    }

    private void Update() {
        if (HasPresentedObject()) {
            switch (State) {
                case EnumState.Idle:
                    break;

                case EnumState.Frying:
                    Heating(EnumState.Fried);

                    break;

                case EnumState.Fried:
                    Heating(EnumState.Burned);

                    break;

                case EnumState.Burned:
                    break;
            }
        }
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

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetPresentedObject()
                        .GetKitchenObjectSO());

                    State = EnumState.Frying;
                    fryingTimer = 0f;
                    Invoke_OnProgressChanged(0f);

                    Debug.Log(this + $": state is {State}");
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

                State = EnumState.Idle;
                fryingTimer = 0f;
                Invoke_OnProgressChanged(0f);

                Debug.Log(this + $": state is {State}");
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

            if (fryingRecipeSO == null) {
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

        if (cuttingRecipeSO != null) {
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

    private void Heating(EnumState nextState) {
        float progress = 0f;

        fryingTimer += Time.deltaTime;

        if (fryingRecipeSO == null) {
            return;
        }

        progress = fryingTimer / fryingRecipeSO.cookingTime;

        if (fryingRecipeSO.cookingTime < fryingTimer) {
            Debug.Log(this + $": {fryingRecipeSO.input} is {nextState}");
            GetPresentedObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
            fryingRecipeSO = GetFryingRecipeSOWithInput(fryingRecipeSO.output);

            State = nextState;
            fryingTimer = 0f;
            progress = 0f;

            Debug.Log(this + $": state is {State}");
        }

        Invoke_OnProgressChanged(progress);
    }

    private void Invoke_OnProgressChanged(float progressNormalized) {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
            progressNormalized = progressNormalized
        });
    }
}
