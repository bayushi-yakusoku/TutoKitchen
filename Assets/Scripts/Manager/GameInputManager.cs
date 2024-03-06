using System;
using UnityEngine;

public class GameInputManager : MonoBehaviour {
    // Make it Singleton:
    public static GameInputManager Instance { get; private set; }

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

        //Debug.Log(this + ": MoveUp    -> " + GetBindingText(EnumBinding.MoveUp));
        //Debug.Log(this + ": MoveDown  -> " + GetBindingText(EnumBinding.MoveDown));
        //Debug.Log(this + ": MoveLeft  -> " + GetBindingText(EnumBinding.MoveLeft));
        //Debug.Log(this + ": MoveRight -> " + GetBindingText(EnumBinding.MoveRight));
        //Debug.Log(this + ": Interact  -> " + GetBindingText(EnumBinding.Interact));
        //Debug.Log(this + ": Alternate -> " + GetBindingText(EnumBinding.Alternate));

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

    public void SetBinding(EnumBinding binding) {

    }
}
