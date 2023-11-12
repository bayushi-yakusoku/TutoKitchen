using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliveredRecipe += Instance_OnDeliveredRecipe;
        DeliveryManager.Instance.OnSpawnNewRecipe += Instance_OnSpawnNewRecipe;

        UpdateVisual();
    }

    private void Instance_OnSpawnNewRecipe(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void Instance_OnDeliveredRecipe(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) {
                continue;
            }

            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeWaiting in DeliveryManager.Instance.GetWaitingRecipesList()) {
            Transform recipe = Instantiate(recipeTemplate, container);
            recipe.gameObject.SetActive(true);
            recipe.GetComponent<DeliveryManagerSingleUI>().SetRecipe(recipeWaiting, Random.Range(5, 20));
        }

    }
}
