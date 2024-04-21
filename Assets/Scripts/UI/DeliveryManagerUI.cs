using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Singleton.OnDeliveredRecipe += DeliveryManager_OnDeliveredRecipe;
        DeliveryManager.Singleton.OnSpawnNewRecipe += DeliveryManager_OnSpawnNewRecipe;

        UpdateVisual();
    }

    private void DeliveryManager_OnSpawnNewRecipe(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManager_OnDeliveredRecipe(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) {
                continue;
            }

            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeWaiting in DeliveryManager.Singleton.GetWaitingRecipesList()) {
            Transform recipe = Instantiate(recipeTemplate, container);
            recipe.gameObject.SetActive(true);
            recipe.GetComponent<DeliveryManagerSingleUI>().SetRecipe(recipeWaiting, Random.Range(5, 20));
        }

    }
}
