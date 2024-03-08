using System;
using System.Net.Security;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour {
    // Make it Singleton:
    public static GameInputManager Instance { get; private set; }

    private const string PLAYER_PREF_BINDINGS = "PlayerPrefBindings";

    private GeneratedPlayerInputActions playerInputActions;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public enum EnumBinding {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        Alternate,
        Pause
    }

    private void Awake() {
        // Singleton simple implementation:
        if (Instance != null) {
            Debug.LogWarning(this + ": There is more than one GameInputManager instance... Destroying this one...");

            Destroy(this.gameObject);
        }

        Instance = this;

        playerInputActions = new();

        if (PlayerPrefs.HasKey(PLAYER_PREF_BINDINGS)) {
            Debug.Log(this + $": get player bindings");

            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREF_BINDINGS));
        }
        
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log(this + ": fire OnPauseAction event");

        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log(this + ": fire OnInteractAlternateAction event");

        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log(this + ": fire OnInteractAction event");

        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(EnumBinding binding) {
        switch (binding) {
            default:
            case EnumBinding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();

            case EnumBinding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();

            case EnumBinding.Alternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

            case EnumBinding.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();

            case EnumBinding.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case EnumBinding.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case EnumBinding.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }
    }

    public void SetBinding(EnumBinding binding, Action onCompleteRebind) {
        playerInputActions.Player.Disable();

        InputAction action;
        int index = 0;

        switch (binding) {
            default:
            case EnumBinding.Pause:
                action = playerInputActions.Player.Pause;
                break;

            case EnumBinding.Interact:
                action = playerInputActions.Player.Interact;
                break;

            case EnumBinding.Alternate:
                action = playerInputActions.Player.InteractAlternate;
                break;

            case EnumBinding.MoveUp:
                action = playerInputActions.Player.Move;
                index = 1;
                break;

            case EnumBinding.MoveDown:
                action = playerInputActions.Player.Move;
                index = 2;
                break;

            case EnumBinding.MoveLeft:
                action = playerInputActions.Player.Move;
                index = 3;
                break;

            case EnumBinding.MoveRight:
                action = playerInputActions.Player.Move;
                index = 4;
                break;
        }

        action.PerformInteractiveRebinding(index)
            .OnComplete(callback => {
                callback.Dispose();

                playerInputActions.Player.Enable();

                PlayerPrefs.SetString(PLAYER_PREF_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                onCompleteRebind();
            }
        )
            .Start();
    }
}
