using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Game Inputs", menuName = "Scriptable Objects/GameInputs")]
public class GameInputs : ScriptableObject
{
    public event Action<Vector2> OnTurnPressed;

    private GameInputActions gameInputActions;

    private void OnTurn(InputAction.CallbackContext context)
    {
        OnTurnPressed?.Invoke(context.ReadValue<Vector2>().normalized);
        Debug.Log(context.ReadValue<Vector2>().normalized);
    }

    private void OnEnable()
    {
        gameInputActions ??= new GameInputActions();

        gameInputActions.Gameplay.Turn.performed += OnTurn;

        gameInputActions.Enable();
    }

    private void OnDisable()
    {
        gameInputActions.Disable();
    }
}
