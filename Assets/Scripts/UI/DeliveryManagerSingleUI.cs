using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Image progressBarImage;

    private RecipeSO recipe;

    private float timer = 1f;
    private float passedTime = 0f;

    private void Start() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Update() {
        passedTime += Time.deltaTime;

        //progressBarImage.fillAmount = passedTime / timer;
        //progressBarImage.fillAmount = 1f / 2;
    }

    public void SetRecipe(RecipeSO recipe, float timer) {
        this.recipe = recipe;
        this.timer = timer;

        recipeNameText.text = recipe.recipeName;

        UpdateIcons();
    }

    private void UpdateIcons() {
        foreach (Transform child in iconContainer) {
            if (child == iconTemplate) {
                continue;
            }

            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipe.ingredientsList) {
            Transform icon = Instantiate(iconTemplate, iconContainer);
            icon.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            icon.gameObject.SetActive(true);
        }
    }
}
