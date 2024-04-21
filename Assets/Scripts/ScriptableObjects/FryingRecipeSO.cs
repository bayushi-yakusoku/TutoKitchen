using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen Chaos/Frying Recipe SO")]
public class FryingRecipeSO : ScriptableObject {
    public KitchenObjectSO input;
    public KitchenObjectSO output;

    public float cookingTime;
}
