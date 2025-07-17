using UnityEngine;

public class ClearCounter : BaseCounter
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public override void Interact(Player player)
	{
		if (!HasKitchenObject())
		{
			//This counter has no kitchen object
			if (player.HasKitchenObject())
			{
				//Player has an object, take it from him and put it on the counter
				player.GetKitchenObject().SetKitchenObjectParent(this);
			}
			else
			{
				//Player has nothing
			}
		}
		else
		{
			if (player.HasKitchenObject())
			{
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DestroySelf();
					}
				}
				else
				{
					if (GetKitchenObject().TryGetPlate(out PlateKitchenObject onCounterPlateKitchenObject)) {
						if (onCounterPlateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
						{
							player.GetKitchenObject().DestroySelf();
						}
					}
				}
			}
			else
			{
				//Player has no object, give this to him.
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}

	public override void InteractAlternate(Player player)
	{
		Debug.Log("Alternate interact, cutting and shit");
	}
}