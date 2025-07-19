using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputsSO playerInputs;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float clampPadding = 1f;

    private Rigidbody body;
    private float movementClampX;
    private float movementClampY;

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

        float minX = -movementClampX + clampPadding;
        float maxX = movementClampX - clampPadding;
        float minY = -movementClampY + clampPadding;
        float maxY = movementClampY - clampPadding;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        body.MovePosition(targetPos);
    }

    private void SetMovementClamps()
    {
        Camera playerCamera = Camera.main;
        
        float playerCameraZ = MathF.Abs(playerCamera.transform.position.z);

        movementClampY = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, playerCameraZ)).y;
        movementClampX = playerCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, playerCameraZ)).x;
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