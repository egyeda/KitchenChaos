using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent
{
	public static Player Instance { get; private set; }


	public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
	public class OnSelectedCounterChangedEventArgs : EventArgs
	{
		public BaseCounter SelectedCounter;
	}


	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float playerRadius = 0.6f;
	[SerializeField] private float playerHeight = 2f;
	[SerializeField] private float interactRange = 2f;
	[SerializeField] private GameInput gameInput;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private Transform kitchenObjectHoldPoint;


	private bool isWalking = false;
	private Vector3 lastInteractedDirection;
	private BaseCounter selectedCounter;
	private KitchenObject kitchenObject;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.Log("There is more that one Player instance");
			return;
		}

		Instance = this;
	}

	private void Start()
	{
		Instance = this;
		gameInput.OnInteractAction += GameInput_OnInteractAction;
		gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
	}

	private void GameInput_OnInteractAction(object sender, EventArgs e)
	{
		if (selectedCounter != null)
		{
			selectedCounter.Interact(this);
		}
	}
	private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
	{
		if (selectedCounter != null)
		{
			selectedCounter.InteractAlternate(this);
		}
	}

	private void Update()
	{
		HandleMovement();
		HandleInteractions();
	}

	public bool IsMoving()
	{
		return isWalking;
	}

	private void HandleInteractions()
	{
		Vector2 movement = gameInput.GetMovementVectorNormalized();

		Vector3 movementDirection = new Vector3(movement.x, 0f, movement.y);

		if (movementDirection != Vector3.zero)
		{
			lastInteractedDirection = movementDirection;
		}

		bool canInteract = Physics.Raycast(transform.position, lastInteractedDirection,
											out RaycastHit lRaycastInfo, interactRange, layerMask);

		if (canInteract)
		{
			if (lRaycastInfo.transform.TryGetComponent(out BaseCounter counter))
			{
				if (counter != selectedCounter)
				{
					SetSelectedCounter(counter);
				}
			}
			else
			{
				SetSelectedCounter(null);
			}
		}
		else
		{
			SetSelectedCounter(null);
		}
	}

	private void SetSelectedCounter(BaseCounter counter)
	{
		selectedCounter = counter;

		OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
		{
			SelectedCounter = counter
		});
	}
	private void HandleMovement()
	{
		Vector2 movement = gameInput.GetMovementVectorNormalized();

		Vector3 movementDirection = new Vector3(movement.x, 0f, movement.y);

		float moveDistance = Time.deltaTime * moveSpeed;
		bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
											playerRadius, movementDirection, moveDistance);

		if (canMove)
		{
			transform.position += movementDirection * moveDistance;
		}
		else
		{
			//Check if the player can move on the x axis
			Vector3 moveDirX = new Vector3(movementDirection.x, 0, 0).normalized;
			canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
											playerRadius, moveDirX, moveDistance);

			if (canMove)
			{
				transform.position += moveDirX * moveDistance;
			}
			else
			{
				//Check if the player can move on the z axis
				Vector3 moveDirZ = new Vector3(0, 0, movementDirection.z).normalized;
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
												playerRadius, moveDirZ, moveDistance);

				if (canMove)
					transform.position += moveDirZ * moveDistance;

			}
		}
		isWalking = movement != Vector2.zero;

		float rotationSpeed = 10f;
		transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
	}

	public Transform GetKitchenObjectFollowTransform()
	{
		return kitchenObjectHoldPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
	}

	public KitchenObject GetKitchenObject()
	{
		return kitchenObject;
	}

	public void ClearKitchenObject()
	{
		kitchenObject = null;
	}

	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}
}