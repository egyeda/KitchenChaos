using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
	private const string CUT = "Cut";
	[SerializeField] CuttingCounter cuttingCounter;

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		cuttingCounter.OnCut += CuttingCounter_OnCut;
	}

	private void CuttingCounter_OnCut(object sender, EventArgs e)
	{
		animator.SetTrigger(CUT);
	}
}