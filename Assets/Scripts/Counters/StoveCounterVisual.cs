using System;
using UnityEngine;
using static StoveCounter;

public class StoveCounterVisual : MonoBehaviour
{
	[SerializeField] private GameObject stoveSizzlingGameObject;
	[SerializeField] private GameObject praticlesGameObject;
	[SerializeField] private StoveCounter stoveCounter;

	private void Start()
	{
		stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
	}

	private void StoveCounter_OnStateChanged(object sender, OnStateChangedEventArgs e)
	{
		bool showVisual = e.state == State.Frying || e.state == State.Fried;
		
		stoveSizzlingGameObject.SetActive(showVisual);
		praticlesGameObject.SetActive(showVisual);
	}
}