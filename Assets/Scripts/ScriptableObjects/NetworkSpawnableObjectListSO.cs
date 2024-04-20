using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen Chaos/Network Spawnable Object List SO")]
public class NetworkSpawnableObjectListSO : ScriptableObject {
    public List<KitchenObjectSO> fullList;
}
