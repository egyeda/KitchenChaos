using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
	public event EventHandler OnRecipeSpawned;
	public event EventHandler OnRecipeCompleted;

	public event EventHandler OnRecipeSuccess;
	public event EventHandler OnRecipeFail;



	public static DeliveryManager Instance
	{
		get;
		private set;
	}



	[SerializeField] private RecipeListSO recipeListSO;



	private static List<RecipeSO> waitingRecipeSOList;
	private float spawnRecipeTimer;
	private float spawnRecipeTimerMax = 3f;
	private int waitingRecipeSOListMax = 4;



	private void Awake()
	{
		Instance = this;
		waitingRecipeSOList = new List<RecipeSO>();
	}
	private void Update()
	{
		spawnRecipeTimer += Time.deltaTime;
		if (spawnRecipeTimer >= spawnRecipeTimerMax)
		{
			spawnRecipeTimer = 0f;

			if (waitingRecipeSOList.Count < waitingRecipeSOListMax)
			{
				RecipeSO recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

				waitingRecipeSOList.Add(recipeSO);

				OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
	{
		for (int waitingRecipeSOIndex = 0; waitingRecipeSOIndex < waitingRecipeSOList.Count; waitingRecipeSOIndex++)
		{
			RecipeSO waitingRecipeSO = waitingRecipeSOList[waitingRecipeSOIndex];

			//Check if the recipe count matches
			if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
			{

				bool plateRecipesMatch = true;
				foreach (KitchenObjectSO waitingKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
				{
					bool ingredientMatches = false;
					foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
					{
						if (plateKitchenObjectSO == waitingKitchenObjectSO)
						{
							ingredientMatches = true;
							break;
						}
					}

					if (!ingredientMatches)
					{
						plateRecipesMatch = false;
					}
				}

				if (plateRecipesMatch)
				{
					waitingRecipeSOList.RemoveAt(waitingRecipeSOIndex);
					OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
					OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
					return;
				}
			}
		}

		OnRecipeFail?.Invoke(this, EventArgs.Empty);
	}

	public List<RecipeSO> GetWaitingRecipeSOList()
	{
		return waitingRecipeSOList;
	}
}