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
        playButton.onClick.AddListener(PlayButton_OnClick);
        quitButton.onClick.AddListener(QuitButton_OnClick);
	}

	private void QuitButton_OnClick()
	{
        Application.Quit();
	}

	private void PlayButton_OnClick()
	{
        SceneManager.LoadScene(1);
	}
}