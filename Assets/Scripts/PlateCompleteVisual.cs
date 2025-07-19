using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
	[Serializable]
	public struct KitchenObjectSO_GameObject
	{
		public KitchenObjectSO kitchenObjectSO;
		public GameObject gameObject;
	}




	[SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectPairs;




	private void Start()
	{
		plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

		foreach (KitchenObjectSO_GameObject iKitchenObjectSO_GameObject in kitchenObjectSOGameObjectPairs) {
			iKitchenObjectSO_GameObject.gameObject.SetActive(false);
		}
	}

	private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
	{
		GameObject currentGameObject = kitchenObjectSOGameObjectPairs
			.First((iKitchenObjectSOGameObject) => iKitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO).gameObject;

		if (currentGameObject != null) {
			currentGameObject.SetActive(true);
		}
	}
}