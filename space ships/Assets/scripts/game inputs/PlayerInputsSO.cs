using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Player Inputs", menuName = "Scriptable Objects/PlayerInputs")]
public class PlayerInputsSO : ScriptableObject
{
    public event Action<Vector2> OnMove;
    public event Action OnFire;

    private PlayerInputActions playerInputActions;

    private void OnMovePressed(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>().normalized);
    }
    
    private void OnFirePressed(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }

    private void OnEnable()
    {
        playerInputActions ??= new PlayerInputActions();

        playerInputActions.Gameplay.Move.performed += OnMovePressed;
        playerInputActions.Gameplay.Fire.performed += OnFirePressed;

        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
