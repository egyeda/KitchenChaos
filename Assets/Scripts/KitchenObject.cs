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
			Debug.LogError("IKitchenObjectParent has another object");
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

	public void DestroySelf()
	{
		kitchenObjectParent.ClearKitchenObject();

		Destroy(gameObject);
	}
	public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
	{
		Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
		
		KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

		kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

		return kitchenObject;
	}
}