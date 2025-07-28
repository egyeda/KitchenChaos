using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance { get; private set; }


	public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternateAction;
	public event EventHandler OnPauseAction;


	private PlayerInputActions playerInputActions;


	private void Awake()
	{
		Instance = this;
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Interact.performed += Interact_Performed;
		playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
		playerInputActions.Player.Pause.performed += Pause_Performed;
	}

	private void Pause_Performed(InputAction.CallbackContext context)
	{
		OnPauseAction?.Invoke(this, EventArgs.Empty);
	}

	private void Interact_Performed(InputAction.CallbackContext context)
	{
		OnInteractAction?.Invoke(this, EventArgs.Empty);
	}

	private void InteractAlternate_Performed(InputAction.CallbackContext context)
	{
		OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVectorNormalized()
	{
		//The value is already normalized by the Input System
		Vector2 movementInput = playerInputActions.Player.Move.ReadValue<Vector2>();

		return movementInput;
	}
}