using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen Chaos/Recipe SO")]
public class RecipeSO : ScriptableObject {
    public string recipeName;

    public List<KitchenObjectSO> ingredientsList;
}
