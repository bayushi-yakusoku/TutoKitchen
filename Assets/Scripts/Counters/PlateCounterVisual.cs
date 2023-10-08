using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour {

    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private GameObject presentedVisual;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private float distanceBetweenPresentedVisuals;

    private List<GameObject> visualPresentedObjectList;

    // Start is called before the first frame update
    void Start() {
        visualPresentedObjectList = new();
        plateCounter.OnSpawnNewPlate += PlateCounter_OnSpawnNewPlate;
        plateCounter.OnRemoveLastPlate += PlateCounter_OnRemoveLastPlate;
    }

    private void PlateCounter_OnRemoveLastPlate(object sender, System.EventArgs e) {
        if (visualPresentedObjectList.Count == 0) {
            return;
        }

        GameObject gameObjectToRemove = visualPresentedObjectList.Last();

        Destroy(gameObjectToRemove);
        visualPresentedObjectList.RemoveAt(visualPresentedObjectList.Count - 1);
    }

    private void PlateCounter_OnSpawnNewPlate(object sender, System.EventArgs e) {
        GameObject newVisual = GameObject.Instantiate(presentedVisual, counterTopPoint);

        newVisual.transform.position += new Vector3(0, distanceBetweenPresentedVisuals * visualPresentedObjectList.Count, 0);
        visualPresentedObjectList.Add(newVisual);
    }

    // Update is called once per frame
    void Update() {

    }
}
