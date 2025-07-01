using UnityEngine;

public class StoveCounter : BaseCounter
{
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
			//This counter has kitchen object
			if (!player.HasKitchenObject())
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