using System;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
	public event EventHandler PlayerInteracted;

	[SerializeField] private KitchenObjectSO kitchenObjectSO;


	public override void Interact(Player player)
	{
		if (!player.HasKitchenObject())
		{
			KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

			PlayerInteracted?.Invoke(this, EventArgs.Empty);
		}
	}
}