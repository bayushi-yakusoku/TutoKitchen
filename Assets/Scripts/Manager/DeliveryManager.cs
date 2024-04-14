using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public sealed class DeliveryManager : NetworkBehaviour {

    // Make it Singleton:
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipesListSO possibleRecipesList;
    [SerializeField] private int maxWaitingRecipes;
    [SerializeField] private float spawnTimerDelay;

    [SerializeField] private DeliveryCounter[] deliveryCounters;

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

        if (!IsServer) {
            return;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTimerDelay) {
            spawnTimer = 0f;
            Spawn();
        }
    }

    private void Spawn() {
        if (KitchenGameManager.Instance.State != KitchenGameManager.EnumState.GamePlaying) {
            return;
        }

        if (waitingRecipesList.Count >= maxWaitingRecipes)
            return;

        Debug.Log(this + ": Server generate new recipe for waiting list");

        int indexRecipeSO = UnityEngine.Random.Range(0, possibleRecipesList.fullList.Count);

        AddChosenRecipeClientRpc(indexRecipeSO);
    }

    [ClientRpc]
    private void AddChosenRecipeClientRpc(int indexRecipeSO) {

        RecipeSO recipeSO = possibleRecipesList.fullList[indexRecipeSO];

        waitingRecipesList.Add(recipeSO);

        OnSpawnNewRecipe?.Invoke(this, EventArgs.Empty);

        Debug.Log(this + ": new recipe waiting " + recipeSO.name);

    }

    public void Deliver(PlateKitchenObject plate, DeliveryCounter counter) {
        List<KitchenObjectSO> plateContent = new(plate.GetPlateContent());

        int indexDeliveryCounters = 0;

        // Find delivery counter:
        for (int index = 0; index < deliveryCounters.Length; index++) {
            if (deliveryCounters[index] == counter) {
                Debug.Log(this + ": delivery Counter found");

                indexDeliveryCounters = index;
                break;
            }
        }

        // Check If delivered recipe is successful or not:
        for (int index = 0; index < waitingRecipesList.Count; index++) {
            RecipeSO recipe = waitingRecipesList[index];

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
                Debug.Log(this + ": successful delivering " + recipe.name);

                SuccessfulDeliveryRpc(index, indexDeliveryCounters);

                return;
            }
        }

        Debug.Log(this + ": failed to deliver an expected recipe");

        FailedDeliveryRpc(indexDeliveryCounters);

    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SuccessfulDeliveryRpc(int indexRecipeSO, int indexDeliveryCounters) {
        waitingRecipesList.RemoveAt(indexRecipeSO);

        OnDeliveredRecipe?.Invoke(deliveryCounters[indexDeliveryCounters], EventArgs.Empty);

        RecipesSuccesfullyDelivered++;

        OnDeliverySuccess?.Invoke(deliveryCounters[indexDeliveryCounters], EventArgs.Empty);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void FailedDeliveryRpc(int indexDeliveryCounters) {
        OnDeliveryFailed?.Invoke(deliveryCounters[indexDeliveryCounters], EventArgs.Empty);
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
