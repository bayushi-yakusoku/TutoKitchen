using Unity.Netcode;
using UnityEngine;

public class MultiplayerManager : NetworkBehaviour {
    // Make it Singleton:
    public static MultiplayerManager Singleton { get; private set; }

    [SerializeField] private NetworkSpawnableObjectListSO spawnableObjectList;

    private void Awake() {
        // Singleton simple implementation:
        if (Singleton != null) {
            Debug.LogWarning(this + ": There is more than one MultiplayerManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Singleton = this;
    }

    public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Debug.Log(this + ": spawn " + kitchenObjectSO);

        SpawnKitchenObjectRpc(GetIndexOfKitchenObjectSO(kitchenObjectSO), kitchenObjectParent.GetNetworkRef());
    }

    [Rpc(SendTo.Server)]
    private void SpawnKitchenObjectRpc(int indexOfKitchenObjectSO, NetworkObjectReference kitchenObjectParent) {
        Debug.Log(this + ": Server Rpc for spawn kitchen object n°" + indexOfKitchenObjectSO);

        KitchenObjectSO kitchenObjectSO = GetKitchenObjectSO(indexOfKitchenObjectSO);
        
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        NetworkObject instantiatedNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
        instantiatedNetworkObject.Spawn();

        //KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        //kitchenObject.Owner = kitchenObjectParent;

    }

    private int GetIndexOfKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
        return spawnableObjectList.fullList.IndexOf(kitchenObjectSO);
    }

    private KitchenObjectSO GetKitchenObjectSO(int index) {
        return spawnableObjectList.fullList[index];
    }
}
