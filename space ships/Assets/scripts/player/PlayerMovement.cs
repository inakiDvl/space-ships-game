using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputsSO playerInputs;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float clampPadding = 1f;

    private Rigidbody body;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private Vector3 movementVector;

    private void CreateMovementVector(Vector2 vector)
    {
        movementVector = (Vector3)vector;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (movementVector == Vector3.zero)
            return;

        Vector3 targetPos = transform.position + speed * Time.fixedDeltaTime * movementVector;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        body.MovePosition(targetPos);
    }

    private void SetMovementClamps()
    {
        Camera playerCamera = Camera.main;

        float playerCameraZ = MathF.Abs(playerCamera.transform.position.z);

        float screenBorderY = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, playerCameraZ)).y;
        float screenBorderX = playerCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, playerCameraZ)).x;
        
        minX = -screenBorderX + clampPadding;
        maxX = screenBorderX - clampPadding;
        minY = -screenBorderY + clampPadding;
        maxY = screenBorderY - clampPadding;
    }

    private void InitializeRigidBody()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
    }

    private void Awake()
    {
        InitializeRigidBody();
        SetMovementClamps();
    }

    private void OnEnable()
    {
        playerInputs.OnMove += CreateMovementVector;
    }

    private void OnDisable()
    {
        playerInputs.OnMove -= CreateMovementVector;
    }
}