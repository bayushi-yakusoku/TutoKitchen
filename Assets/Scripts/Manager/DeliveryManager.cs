using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class DeliveryManager : MonoBehaviour {

    // Make it Singleton:
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipesListSO possibleRecipesList;
    [SerializeField] private int maxWaitingRecipes;
    [SerializeField] private float spawnTimerDelay;

    public event EventHandler OnSpawnNewRecipe;
    public event EventHandler OnDeliveredRecipe;

    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;

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

        RecipeSO recipeSO = possibleRecipesList.fullList[UnityEngine.Random.Range(0,
            possibleRecipesList.fullList.Count)];

        waitingRecipesList.Add(recipeSO);

        OnSpawnNewRecipe?.Invoke(this, EventArgs.Empty);

        Debug.Log(this + ": waiting for a " + recipeSO.name);
    }

    public void Deliver(PlateKitchenObject plate, DeliveryCounter counter) {
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

                OnDeliveredRecipe?.Invoke(counter, EventArgs.Empty);

                RecipesSuccesfullyDelivered++;

                OnDeliverySuccess?.Invoke(counter, EventArgs.Empty);

                return;
            }
        }

        Debug.Log(this + ": failed to deliver an expected recipe");

        OnDeliveryFailed?.Invoke(counter, EventArgs.Empty);

    }

    public List<RecipeSO> GetWaitingRecipesList() {
        return waitingRecipesList;
    }

    private int _recipesSuccesfullyDelivered = 0;
    public int RecipesSuccesfullyDelivered {
        get => _recipesSuccesfullyDelivered;
        private set { 
            _recipesSuccesfullyDelivered = value; 
        }
    }
}
