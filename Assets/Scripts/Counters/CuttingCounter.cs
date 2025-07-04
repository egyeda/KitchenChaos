using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
	public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
	public class OnProgressChangedEventArgs : EventArgs
	{
		public float progressNormalized;
	}

	public event EventHandler OnCut;

	[SerializeField] CuttingRecipeSO[] CuttingRecipeSOs;
	private int cuttingProgress;

	public override void Interact(Player player)
	{
		if (!HasKitchenObject())
		{
			//This counter has no kitchen object
			if (player.HasKitchenObject())
			{
				// Check if pplayer has an object iwth recipe and can be cut
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
				{
					//Put it on the counter
					player.GetKitchenObject().SetKitchenObjectParent(this);
					this.cuttingProgress = 0;

					CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());

					SetProgress(cuttingRecipeSO);
				}
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

	private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
	{
		return GetCuttingRecipeSO(kitchenObjectSO) == null ? false : true;
	}

	public override void InteractAlternate(Player player)
	{
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
		{
			cuttingProgress++;
			OnCut?.Invoke(this, EventArgs.Empty);
			
			CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());

			SetProgress(cuttingRecipeSO);

			if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
			{
				KitchenObject currentKitchenObject = GetKitchenObject();

				KitchenObjectSO kitchenObjectToSpawn = GetOutputForInput(currentKitchenObject.GetKitchenObjectSO());
				currentKitchenObject.DestroySelf();

				KitchenObject.SpawnKitchenObject(kitchenObjectToSpawn, this);
			}
		}
	}

	private CuttingRecipeSO SetProgress(CuttingRecipeSO cuttingRecipeSO)
	{
		float progress = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;
		OnProgressChangedEventArgs progressEventArgs = new OnProgressChangedEventArgs
		{
			progressNormalized = progress
		};

		OnProgressChanged?.Invoke(this, progressEventArgs);
		return cuttingRecipeSO;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
	{
		return GetCuttingRecipeSO(input).output;
	}

	private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO input)
	{
		foreach (CuttingRecipeSO cuttingRecipeSO in CuttingRecipeSOs)
		{
			if (cuttingRecipeSO.input == input)
			{
				return cuttingRecipeSO;
			}
		}

		return null;
	}
}