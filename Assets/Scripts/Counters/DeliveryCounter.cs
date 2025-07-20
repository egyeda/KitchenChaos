using UnityEngine;

public class DeliveryCounter : BaseCounter
{
	
	public override void Interact(Player player)
	{
		if (player.HasKitchenObject())
		{
			if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
			{
				DeliveryManager.DeliverRecipe(plateKitchenObject);
				player.GetKitchenObject().DestroySelf();
			}
		}
	}
}