using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
	public static event EventHandler OnAnyCut;
	new public static void ResetStaticData()
	{
		OnAnyCut = null;
	}

	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

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
			else
			{
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
						GetKitchenObject().DestroySelf();
					}
				}
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
			OnAnyCut?.Invoke(this, EventArgs.Empty);
			
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
		IHasProgress.OnProgressChangedEventArgs progressEventArgs = new IHasProgress.OnProgressChangedEventArgs
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