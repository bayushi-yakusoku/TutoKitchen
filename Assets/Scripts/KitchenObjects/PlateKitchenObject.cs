using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> authorizedKitchenObjects;

    private List<KitchenObjectSO> plateContent;

    public class AddIngredientEventArgument : EventArgs {
        public KitchenObjectSO ingredient;
    }

    public event EventHandler<AddIngredientEventArgument> addIngredientEvent;

    private void Start() {
        plateContent = new();
    }

    public bool TryAddIngredient(KitchenObjectSO ingredient) {
        if (! authorizedKitchenObjects.Contains(ingredient)) {
            Debug.Log(this + ": unauthorized ingredient");

            return false;
        }

        if (plateContent.Contains(ingredient)) {
            Debug.Log(this + ": ingredient already present");

            return false;
        }

        Debug.Log(this + ": ingredient added");

        plateContent.Add(ingredient);
        addIngredientEvent?.Invoke(this, new AddIngredientEventArgument {
            ingredient = ingredient
        }) ;

        return true;
    }

    public List<KitchenObjectSO> GetPlateContent() {
        return plateContent;
    }
}
