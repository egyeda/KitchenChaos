using UnityEngine;

public class KitchenObject : MonoBehaviour
{
	private IKitchenObjectParent kitchenObjectParent;
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public KitchenObjectSO GetKitchenObjectSO()
	{
		return kitchenObjectSO;
	}

	public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
	{
		if (this.kitchenObjectParent != null)
		{
			this.kitchenObjectParent.ClearKitchenObject();
		}

		if (kitchenObjectParent.HasKitchenObject())
		{
			Debug.LogError("kitchenObjectParent has another object");
			return;
		}
		
		this.kitchenObjectParent = kitchenObjectParent;
		this.kitchenObjectParent.SetKitchenObject(this);

		transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
		transform.localPosition = Vector3.zero;
	}

	public IKitchenObjectParent GetKitchenObjectParent()
	{
		return this.kitchenObjectParent;
	}
}
