using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectsButtonText;
    [SerializeField] private TextMeshProUGUI musicButtonText;


    private void Awake()
    {
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {

        });
    }

    private void UpdateVisual()
    {
        soundEffectsButtonText.text = "Sound effects: " + Math.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
    }
}
