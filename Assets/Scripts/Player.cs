using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;

public sealed class Player : MonoBehaviour, IKitchenObjectParent {
    // Make it Singleton:
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private float playerRadius = 1f;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask couterLayerMask;

    [SerializeField] private Transform presentingPoint;

    private bool isWalking = false;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject presentedObject;

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    private void Awake() {
        // Singleton simple implementation:
        if (Instance != null) {
            Debug.LogWarning(this + ": There is more than one Player instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void Update() {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        isWalking = inputVector != Vector2.zero;

        if (!isWalking) {
            return;
        }

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

        float moveDistance = moveSpeed * Time.deltaTime;

        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveDir,
                moveDistance
            );

        if (!canMove) {
            Vector3 moveX = new Vector3(moveDir.x, 0f, 0f);
            canMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveX,
                    moveDistance
                );

            if (canMove) {
                moveDir = moveX.normalized;
            }
            else {
                Vector3 moveZ = new Vector3(0f, 0f, moveDir.z);
                canMove = !Physics.CapsuleCast(
                        transform.position,
                        transform.position + Vector3.up * playerHeight,
                        playerRadius,
                        moveZ,
                        moveDistance
                    );

                if (canMove) {
                    moveDir = moveZ.normalized;
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }
    }

    private void HandleInteraction() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDirection = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(
                    transform.position,
                    lastInteractDirection,
                    out RaycastHit raycastHit,
                    interactDistance,
                    couterLayerMask
                )
            ) {

            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                if (selectedCounter != baseCounter) {
                    SetSelectedCounter(baseCounter);
                }
            }
            else {
                SetSelectedCounter(null);
            }
        }
        else {
            SetSelectedCounter(null);
        }

    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        Debug.Log(this + ": received GameInput_OnInteractAction event");
        
        if (selectedCounter) {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        Debug.Log(this + ": received GameInput_OnInteractAlternateAction event");
        
        if (selectedCounter) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void SetSelectedCounter(BaseCounter counter) {
        selectedCounter = counter;

        OnSelectedCounterChanged?.Invoke(
                this,
                new OnSelectedCounterChangedEventArgs {
                    selectedCounter = selectedCounter
                }
            );
    }

    public Transform GetKitchenObjectFollowTransform() {
        return presentingPoint;
    }

    public void SetPresentedObject(KitchenObject kitchenObject) {
        presentedObject = kitchenObject;
    }

    public KitchenObject GetPresentedObject() {
        return presentedObject;
    }

    public void ClearPresentedObject() {
        presentedObject = null;
    }

    public bool HasPresentedObject() {
        return presentedObject != null;
    }
}
