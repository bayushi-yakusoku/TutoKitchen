using System;
using UnityEngine;

public class PlateCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO objectToGive;
    [SerializeField] private float spawnTimer;
    [SerializeField] private int maxPresentedObjects;


    private int numberOfSpawnedPlates = 0;

    private float timer = 0;

    private bool maxAlertPrinted = false;

    public event EventHandler OnSpawnNewPlate;
    public event EventHandler OnRemoveLastPlate;

    private void Start() {

    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer >= spawnTimer) {
            timer = 0;
            SpawnPlate();
        }
    }

    public override void Interact(Player player) {
        if (!player.HasPresentedObject()) {
            if (numberOfSpawnedPlates > 0) {
                Debug.Log(this + ": removing a plate");

                numberOfSpawnedPlates--;
                maxAlertPrinted = false;
                timer = 0;
                OnRemoveLastPlate?.Invoke(this, EventArgs.Empty);

                KitchenObject.SpawnKitchenObject(objectToGive, player);
            }
        }
    }

    private void SpawnPlate() {
        if (KitchenGameManager.Instance.State != KitchenGameManager.EnumState.GamePlaying) {
            return;
        }

        if (numberOfSpawnedPlates >= maxPresentedObjects) {
            if (!maxAlertPrinted) { // to avoid flooding log...
                Debug.Log(this + ": maximum visual presented object reached");
                maxAlertPrinted = true;
            }

            return;
        }

        Debug.Log(this + ": Spawn visual presented object");
        numberOfSpawnedPlates++;

        OnSpawnNewPlate?.Invoke(this, EventArgs.Empty);
    }
}
