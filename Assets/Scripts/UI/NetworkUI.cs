using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour {
    [SerializeField] private Button hostButton;
    [SerializeField] private Button serverButton;
    [SerializeField] private Button clientButton;


    private void Start() {
        hostButton.onClick.AddListener(HostButtonClick);
        //serverButton.onClick.AddListener(ServerButtonClick);
        clientButton.onClick.AddListener(ClientButtonClick);

        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
        hostButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void HostButtonClick() {
        Debug.Log(this + ": Start Host");

        NetworkManager.Singleton.StartHost();
        Hide();
    }

    private void ServerButtonClick() {
        Debug.Log(this + ": Start Server");

        NetworkManager.Singleton.StartServer();
        Hide();
    }

    private void ClientButtonClick() {
        Debug.Log(this + ": Start Client");

        NetworkManager.Singleton.StartClient();
        Hide();
    }

}
