using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO objectToGive;
    [SerializeField] private float spawnTimer;
    [SerializeField] private int maxPresentedObjects;


    private int numberOfSpawnedPlates = 0;

    private float timer = 0;

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
                timer = 0;
                OnRemoveLastPlate?.Invoke(this, EventArgs.Empty);

                KitchenObject.SpawnKitchenObject(objectToGive, player);
            }
        }
    }

    private void SpawnPlate() {
        if (numberOfSpawnedPlates >= maxPresentedObjects) {
            Debug.Log(this + ": maximum visual presented object reached");

            return;
        }

        Debug.Log(this + ": Spawn visual presented object");
        numberOfSpawnedPlates++;

        OnSpawnNewPlate?.Invoke(this, EventArgs.Empty);
    }
}
