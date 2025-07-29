using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IUpdateable
{
    [SerializeField] private PlayerInputsSO playerInputs;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float screenMargin = 1f;

    private Rigidbody body;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private Vector3 movementVector;

    public void DoUpdate(float deltaTime)
    {
        MovePlayer();
    }

    private void CreateMovementVector(Vector2 vector)
    {
        movementVector = (Vector3)vector;
    }

    private void MovePlayer()
    {
        if (movementVector == Vector3.zero)
            return;

        Vector3 targetPos = transform.position + speed * Time.deltaTime * movementVector;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        transform.position = targetPos;
    }

    private void SetMovementClamps()
    {
        Camera playerCamera = Camera.main;

        float playerCameraZ = MathF.Abs(playerCamera.transform.position.z);

        float screenBorderY = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, playerCameraZ)).y;
        float screenBorderX = playerCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, playerCameraZ)).x;
        
        minX = -screenBorderX + screenMargin;
        maxX = screenBorderX - screenMargin;
        minY = -screenBorderY + screenMargin;
        maxY = screenBorderY - screenMargin;
    }

    private void Awake()
    {
        Updater.Instance.AddUpdateable(this);

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