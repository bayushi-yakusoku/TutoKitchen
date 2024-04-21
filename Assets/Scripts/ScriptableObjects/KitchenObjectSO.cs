using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen Chaos/Kitchen Object SO")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
