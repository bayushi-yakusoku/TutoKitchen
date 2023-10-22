using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> authorizedKitchenObjects;

    private List<KitchenObjectSO> plateContent;

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

        return true;
    }
}
