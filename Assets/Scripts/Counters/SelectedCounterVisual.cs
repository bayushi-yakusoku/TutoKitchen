using Unity.Netcode;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] visualGameObjectArray;

    private void Start() {
        NetworkManager.Singleton.OnConnectionEvent += NetworkManager_OnConnectionEvent;
    }

    private void NetworkManager_OnConnectionEvent(NetworkManager arg1, ConnectionEventData arg2) {
        // Looking for the local player instance:
        if (arg2.EventType == ConnectionEvent.ClientConnected) {
            if (NetworkManager.Singleton.LocalClient.PlayerObject != null) {
                Player player = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject.GetComponent<Player>();

                player.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;

                // Deactivate event
                NetworkManager.Singleton.OnConnectionEvent -= NetworkManager_OnConnectionEvent;
            }
        }
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            Debug.Log(this + ": Couter selected");
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(false);
        }
    }
}
