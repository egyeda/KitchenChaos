using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	public event EventHandler OnInteractAction;
	private PlayerInputActions playerInputActions;
	private void Awake()
	{
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Interact.performed += Interacted;
	}

	private void Interacted(InputAction.CallbackContext context)
	{
		OnInteractAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVectorNormalized()
	{
		//The value is already normalized by the Input System
		Vector2 movementInput = playerInputActions.Player.Move.ReadValue<Vector2>();

		return movementInput;
	}
}