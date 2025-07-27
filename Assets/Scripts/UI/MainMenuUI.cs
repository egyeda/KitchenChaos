using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

	private void Awake()
	{
		//Instead of using separate method it can be used by a lambda experssion.
        playButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.GameScene);
		});

		//Separate method for more complex functionalities.
        quitButton.onClick.AddListener(QuitButton_OnClick);
	}

	private void QuitButton_OnClick()
	{
        Application.Quit();
	}
}