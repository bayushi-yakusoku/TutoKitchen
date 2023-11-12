using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        plateKitchenObject.addIngredientEvent += PlateKitchenObject_addIngredientEvent;
    }

    private void PlateKitchenObject_addIngredientEvent(object sender, PlateKitchenObject.AddIngredientEventArgument e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach(Transform child in transform) {
            if (child == iconTemplate) {
                continue;
            }

            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetPlateContent()) {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
