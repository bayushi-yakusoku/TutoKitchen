using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObjectVisual : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    [Serializable]
    public struct IngredientVisual {
        public KitchenObjectSO ingredient;
        public GameObject visual;
    }

    [SerializeField] private List<IngredientVisual> ingredientVisualList;

    private void Start() {
        plateKitchenObject.addIngredientEvent += PlateKitchenObject_addIngredientEvent;

        foreach (IngredientVisual ingredientVisual in ingredientVisualList) {
            ingredientVisual.visual.SetActive(false);
        }
    }

    private void PlateKitchenObject_addIngredientEvent(object sender, PlateKitchenObject.AddIngredientEventArgument e) {
        Debug.Log(this + ": Add ingredient: " + e.ingredient);

        foreach (IngredientVisual ingredientVisual in ingredientVisualList) {
            if (ingredientVisual.ingredient == e.ingredient) {
                Debug.Log(this + ": activate visual: " + ingredientVisual.visual);

                ingredientVisual.visual.SetActive(true);
            }
        }
    }
}
