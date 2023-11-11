using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DeliveryManager : MonoBehaviour {

    // Make it Singleton:
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipesListSO possibleRecipesList;
    [SerializeField] private int maxWaitingRecipes;
    [SerializeField] private float spawnTimerDelay;

    private List<RecipeSO> waitingRecipesList;
    private float spawnTimer = 0f;

    private void Awake() {
        // Singleton simple implementation:
        if (Instance != null) {
            Debug.LogWarning(this + ": There is more than one DeliveryManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        waitingRecipesList = new();

        Instance = this;
    }

    private void Update() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTimerDelay) {
            spawnTimer = 0f;
            Spawn();
        }
    }

    private void Spawn() {
        if (waitingRecipesList.Count >= maxWaitingRecipes)
            return;

        RecipeSO recipeSO = possibleRecipesList.fullList[Random.Range(0,
            possibleRecipesList.fullList.Count)];

        waitingRecipesList.Add(recipeSO);
        Debug.Log(this + ": waiting for a " + recipeSO.name);
    }

    public void Deliver(PlateKitchenObject plate) {
        List<KitchenObjectSO> plateContent = new(plate.GetPlateContent());

        foreach (RecipeSO recipe in waitingRecipesList) {
            if (recipe.ingredientsList.Count != plateContent.Count)
                continue;

            bool found = true;

            foreach (KitchenObjectSO ingredient in recipe.ingredientsList) {
                if (!plateContent.Remove(ingredient)) {
                    found = false;
                    break;
                }
            }

            if (plateContent.Count > 0) {
                continue;
            }

            if (found) {
                Debug.Log(this + ": delivering " + recipe.name);

                waitingRecipesList.Remove(recipe);
                return;
            }
        }

        Debug.Log(this + ": failed to deliver an expected recipe");

    }
}