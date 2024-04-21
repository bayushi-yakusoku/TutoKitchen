using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen Chaos/Network Spawnable Objects List SO")]
public class NetworkSpawnableObjectListSO : ScriptableObject {
    public List<KitchenObjectSO> fullList;
}
