using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
	[SerializeField] private StoveCounter stoveCounter;
	private AudioSource audioSource;
	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
	}

	private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
	{
		bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
		if (playSound)
		{
			if (!audioSource.isPlaying)
				audioSource.Play();
		}
		else
		{
			if (audioSource.isPlaying)
				audioSource.Stop();
		}
	}
}