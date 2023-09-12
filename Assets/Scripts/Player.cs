using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private float playerRadius = 1f;

    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        isWalking = inputVector != Vector2.zero;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        float playerHeight = 2f;

        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);
        bool canMove = !Physics.CapsuleCast(
            transform.position, 
            transform.position + Vector3.up * playerHeight, 
            playerRadius, 
            moveDir, 
            moveDistance);

        if (!canMove)
        {
            Vector3 moveX = new Vector3(moveDir.x, 0f , 0f);
            canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveX,
                moveDistance);

            if (canMove)
            {
                moveDir = moveX.normalized;
            }
            else
            {
                Vector3 moveZ = new Vector3(0f, 0f, moveDir.z);
                canMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveZ,
                    moveDistance);

                if (canMove)
                {
                    moveDir = moveZ.normalized;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
