using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
	[SerializeField] private PlatesCounter platesCounter;


	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualPrefab;


	private List<GameObject> plateVisualGameObjectList;


	void Awake()
	{
		plateVisualGameObjectList = new List<GameObject>();
	}

	private void Start()
	{
		platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
	}

	private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
	{
		GameObject lastPlateVisualGameObject = plateVisualGameObjectList.Last();
		plateVisualGameObjectList.Remove(lastPlateVisualGameObject);
		Destroy(lastPlateVisualGameObject);
	}

	private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
	{
		Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

		float plateOffsetY = 0.1f;
		plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

		plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
	}
}