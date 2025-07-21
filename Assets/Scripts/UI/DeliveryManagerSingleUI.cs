using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI recipeNameText;


	[SerializeField] private Transform iconContainer;
	[SerializeField] private Transform iconTemplate;


	private void Awake()
	{
		iconTemplate.gameObject.SetActive(false);
	}

	public void SetRecipeSO(RecipeSO recipeSO)
	{
		recipeNameText.text = recipeSO.recipeName;
		UpdateVisual(recipeSO);
	}

	private void UpdateVisual(RecipeSO recipeSO)
	{
		foreach (Transform child in iconContainer)
		{
			if (child == iconTemplate) continue;

			Destroy(child.gameObject);
		}

		foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
		{
			Transform iconTranform = Instantiate(iconTemplate, iconContainer);
			iconTranform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
			iconTranform.gameObject.SetActive(true);
		}
	}
}