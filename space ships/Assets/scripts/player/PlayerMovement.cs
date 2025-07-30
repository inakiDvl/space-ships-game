using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IUpdateable
{
    [SerializeField] private GlobalVariablesSO globalVariables;
    [SerializeField] private PlayerInputsSO playerInputs;
    [SerializeField] private float startingMarginX;
    [SerializeField] private float speed = 8f;

    private float maxX;
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

        targetPos.x = Mathf.Clamp(targetPos.x, -maxX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, -maxY, maxY);

        transform.position = targetPos;
    }

    private void SetMaxPositions()
    {
        maxX = globalVariables.MaxX - globalVariables.ScreenMargin;
        maxY = globalVariables.MaxY - globalVariables.ScreenMargin;
    }

    private void SetPlayerStartingPosition()
    {
        Vector3 startingPosition = new(-globalVariables.MaxX + startingMarginX, 0, 0);
        transform.position = startingPosition;
    }

    private void Awake()
    {
        SetMaxPositions();
        SetPlayerStartingPosition();
    }

    private void Start()
    {
        Updater.Instance.AddUpdateable(this);
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