using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
	
	public event EventHandler OnCounterOpened;


	[SerializeField] private KitchenObjectSO kitchenObjectSO;


	public override void Interact(Player player)
	{
		Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
		kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
		
		OnCounterOpened?.Invoke(this, EventArgs.Empty);
	}
}