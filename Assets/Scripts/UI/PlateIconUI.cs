using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour {
    [SerializeField] private Image icon;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
        icon.sprite = kitchenObjectSO.sprite;
    }
}
