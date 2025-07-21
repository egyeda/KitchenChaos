using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{

	public static event EventHandler OnAnyObjectTrashed;



	public override void Interact(Player player)
	{
		if (player.HasKitchenObject())
		{
			OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
			player.GetKitchenObject().DestroySelf();
		}
	}
}
