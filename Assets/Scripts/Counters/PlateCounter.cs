using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter {
    [SerializeField] private GameObject presentedVisual;
    [SerializeField] private float spawnTimer;
    [SerializeField] private int maxPresentedObjects;
    [SerializeField] private float distanceBetweenPresentedVisuals;

    private List<GameObject> visualPresentedObjectList;
    private float timer = 0;


    public override void Interact(Player player) {
        base.Interact(player);
    }

    private void Start() {
        visualPresentedObjectList = new();
    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer >= spawnTimer) {
            timer = 0;
            SpawnVisuals();
        }
    }

    private void SpawnVisuals() {
        if (visualPresentedObjectList.Count >= maxPresentedObjects) {
            Debug.Log(this + ": maximum visual presented object reached");

            return;
        }

        Debug.Log(this + ": Spawn visual presented object");

        GameObject newVisual = GameObject.Instantiate(presentedVisual, GetKitchenObjectFollowTransform());

        newVisual.transform.position += new Vector3(0, distanceBetweenPresentedVisuals * visualPresentedObjectList.Count, 0);
        visualPresentedObjectList.Add(newVisual);
    }
}
