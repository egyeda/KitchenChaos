using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private Transform counterTopPoint;



	private KitchenObject kitchenObject;


	public static event EventHandler OnObjectPlaced;



	public virtual void Interact(Player player)
	{
		Debug.LogError("This should never trigger. BaseCounter.Interact");
	}

	public virtual void InteractAlternate(Player player)
	{
		// Debug.LogError("This should never trigger. BaseCounter.InteractAlternate");
	}

	public Transform GetKitchenObjectFollowTransform()
	{
		return counterTopPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		if (kitchenObject != null) {
			OnObjectPlaced?.Invoke(this, EventArgs.Empty);
		}

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
