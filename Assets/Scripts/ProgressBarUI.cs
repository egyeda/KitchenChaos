using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
	[SerializeField] private CuttingCounter cuttingCounter;
	[SerializeField] private Image progressImage;

	private void Start()
	{
		cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

		progressImage.fillAmount = 0f;

		Hide();
	}

	private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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