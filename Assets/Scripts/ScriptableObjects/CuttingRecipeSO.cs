using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen Chaos/Cutting Recipe SO")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int CutCountNeeded;
}
