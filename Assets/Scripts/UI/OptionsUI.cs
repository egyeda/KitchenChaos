using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }


    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private TextMeshProUGUI soundEffectsButtonText;
    [SerializeField] private TextMeshProUGUI musicButtonText;


    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        
        returnButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    
	private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

	private void GameManager_OnGameUnpaused(object sender, EventArgs e)
	{
        Hide();
	}

	private void UpdateVisual()
    {
        soundEffectsButtonText.text = "SOUND EFFECTS: " + Math.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicButtonText.text = "MUSIC: " + Math.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}