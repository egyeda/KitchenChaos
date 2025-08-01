using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
	[SerializeField] private BaseCounter counter;
	[SerializeField] private GameObject[] visualGameObjects;
	private void Start()
	{
		Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
	}

	private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
	{
		if (e.SelectedCounter == counter)
		{
			Show();
		}
		else
		{
			Hide();	
		}
	}

	private void Show()
	{
		foreach (GameObject visualGameObject in visualGameObjects) {
			visualGameObject.SetActive(true);
		}
	}
	private void Hide()
	{
		foreach (GameObject visualGameObject in visualGameObjects) {
			visualGameObject.SetActive(false);
		}
	}
}