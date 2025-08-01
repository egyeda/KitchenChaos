using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
	[SerializeField] private GameObject hasProgressGameObject;
	[SerializeField] private Image progressImage;


	private IHasProgress hasProgress;

	private void Start()
	{
		hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
		if (hasProgress == null)
		{
			Debug.LogError($"Game object {hasProgressGameObject} does not have a component that implements IHasProgress!");
		}			

		hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

		progressImage.fillAmount = 0f;

		Hide();
	}

	private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
	{
		progressImage.fillAmount = e.progressNormalized;
		if (progressImage.fillAmount <= 0 || progressImage.fillAmount >= 1f)
		{
			Hide();
		}
		else
		{
			Show();
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