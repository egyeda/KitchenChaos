using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    
	private void Start()
	{
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
	}

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
	}
    
	private void Show()
    {
        gameObject.SetActive(true);
    }

	private void Hide()
	{
        gameObject.SetActive(false);
	}
}