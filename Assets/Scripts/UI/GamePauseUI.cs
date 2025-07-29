using System;
using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
	private void Start()
	{
		Hide();
		GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
		GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
	}

	private void GameManager_OnGameUnpaused(object sender, EventArgs e)
	{
		Hide();
	}

	private void GameManager_OnGamePaused(object sender, EventArgs e)
	{
		Show();
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