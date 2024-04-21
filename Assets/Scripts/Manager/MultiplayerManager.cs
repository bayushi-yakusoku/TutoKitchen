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
    private void SpawnKitchenObjectRpc(int indexOfKitchenObjectSO, NetworkObjectReference followerTarget) {
        Debug.Log(this + ": Server - for spawn kitchen object n°" + indexOfKitchenObjectSO);

        KitchenObjectSO kitchenObjectSO = GetKitchenObjectSO(indexOfKitchenObjectSO);
        
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        NetworkObject instantiatedNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
        instantiatedNetworkObject.Spawn();

        SetFollowTargetRpc(instantiatedNetworkObject, followerTarget);

    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void SetFollowTargetRpc(NetworkObjectReference followerRef, NetworkObjectReference targetRef) {
        Debug.Log(this + ": ClientsAndHost - Set follow target: " + followerRef + " targeting: " + targetRef);

        if (! followerRef.TryGet(out NetworkObject followerObject)) {
            Debug.LogError(this + ": ClientsAndHost - cannot find corresponding follower network object");
            
            return;
        }

        if (!targetRef.TryGet(out NetworkObject targetObject)) {
            Debug.LogError(this + ": ClientsAndHost - cannot find corresponding target network object");

            return;
        }

        IKitchenObjectParent target = targetObject.GetComponent<IKitchenObjectParent>();

        KitchenObject follower = followerObject.GetComponent<KitchenObject>();
        follower.Owner = target;
    }

    private int GetIndexOfKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
        return spawnableObjectList.fullList.IndexOf(kitchenObjectSO);
    }

    private KitchenObjectSO GetKitchenObjectSO(int index) {
        return spawnableObjectList.fullList[index];
    }
}
