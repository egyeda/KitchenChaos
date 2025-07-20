using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
	public DeliveryManager Instance
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
				RecipeSO recipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
				
				Debug.Log(recipeSO.recipeName);
				waitingRecipeSOList.Add(recipeSO);
			}
		}
	}

	public static void DeliverRecipe(PlateKitchenObject plateKitchenObject)
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
					Debug.Log("yay");
					return;
				}
			}
		}
		
		Debug.Log("no matches found");
	}
}