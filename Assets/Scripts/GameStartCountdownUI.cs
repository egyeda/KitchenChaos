using System;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

	private void Start()
	{
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
	}

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
	}
	private void Update()
	{
        countdownText.text = Math.Ceiling(GameManager.Instance.GetCountdownToStartTimer()).ToString();
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